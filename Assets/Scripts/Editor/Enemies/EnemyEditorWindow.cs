namespace Assets.Scripts.Editor.Enemies
{
    using System;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    public class EnemyEditorWindow : EditorWindow
    {
        private static string EnemyPath = "Assets/Data/Resources/Enemies";

        public int index = 0;

        private static DisplayData currentDisplayData;

        [MenuItem("Editors/Enemy Editor")]
        public static void Init()
        {
            EnemyEditorWindow window = (EnemyEditorWindow)EditorWindow.GetWindow(typeof(EnemyEditorWindow));
            window.Show();
        }

        private void OnGUI()
        {
            // Events

            this.HandleEvents();


            // Stuff
            string[] enemies = AssetDatabase.FindAssets("", new string[] { EnemyPath });

            var paths = enemies.Select(x => AssetDatabase.GUIDToAssetPath(x)).ToList();

            var names =
                paths.Select(x => x.Split(Convert.ToChar("/")).Last().Split(Convert.ToChar(".")).First()).ToArray();



            index = EditorGUILayout.Popup(index, names);

            DisplayEnemy(paths[index]);
        }

        // Used to handle selecting sprites
        private void HandleEvents()
        {
            Event e = Event.current;
            if (e.type == EventType.ExecuteCommand && e.commandName == "ObjectSelectorUpdated")
            {
                Sprite pickedSprite = EditorGUIUtility.GetObjectPickerObject() as Sprite;

                currentDisplayData.Image = pickedSprite;

                this.Repaint();


            }
            else if (e.type == EventType.ExecuteCommand && e.commandName == "ObjectSelectorClosed")
            {
                
                var picker = EditorGUIUtility.GetObjectPickerObject();
            }

        }

        private void DisplayEnemy(string s)
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(s);

            // References to components on the enemy
            Actor act = prefab.GetComponent<Actor>();
            Damagable damage = prefab.GetComponent<Damagable>();
            ActorAttack attack = prefab.GetComponent<ActorAttack>();
            CountdownBehaviour countdown = prefab.GetComponent<CountdownBehaviour>();

            currentDisplayData = prefab.GetComponent<DisplayData>();

            // Display image
            float size = 200f;
            var rect = EditorGUILayout.GetControlRect(false, size);

            float min = Mathf.Min(rect.width, rect.height) - 20;

            GUI.DrawTexture(new Rect(rect.x, rect.y, min, min), currentDisplayData.Image.texture);

            
            //Button to change sprite of the enemy
            if (GUILayout.Button("Change sprite"))
            {
                EditorGUIUtility.ShowObjectPicker<Sprite>(currentDisplayData.Image, false, "", 0);
            }

            // Change stats of the enemy
            SerializedObject sDamage = new SerializedObject(damage);
            SerializedObject sAttack = new SerializedObject(attack);
            SerializedObject sCountdown = new SerializedObject(countdown);
            SerializedObject sDisplayData = new SerializedObject(currentDisplayData);

            SerializedProperty oldHealth = sDamage.FindProperty("BaseHealth");
            SerializedProperty healthPerRankProp = sDamage.FindProperty("healthPerRank");
            SerializedProperty attackProp = sAttack.FindProperty("StartingBaseAttack");
            SerializedProperty attackPerRankProp = sAttack.FindProperty("attackPerRank");
            SerializedProperty countdownProp = sCountdown.FindProperty("StartMaxCountdown");
            SerializedProperty description = sDisplayData.FindProperty("Description");

            description.stringValue = EditorGUILayout.TextField(
                "Description",
                description.stringValue, GUILayout.Height(100));


            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            oldHealth.intValue = EditorGUILayout.IntField("Base Health", oldHealth.intValue, EditorStyles.numberField, GUILayout.ExpandWidth(false));
            healthPerRankProp.intValue = EditorGUILayout.IntField("Per Rank", healthPerRankProp.intValue, EditorStyles.numberField, GUILayout.ExpandWidth(false));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            attackProp.intValue = EditorGUILayout.IntField("Base Attack", attackProp.intValue, GUILayout.ExpandWidth(false));
            attackPerRankProp.intValue = EditorGUILayout.IntField("Per Rank", attackPerRankProp.intValue, GUILayout.ExpandWidth(false));
            EditorGUILayout.EndHorizontal();

            countdownProp.intValue = EditorGUILayout.IntField("Countdown", countdownProp.intValue, GUILayout.ExpandWidth(false));

            sDamage.ApplyModifiedProperties();
            sAttack.ApplyModifiedProperties();
            sCountdown.ApplyModifiedProperties();
        }

    }
}