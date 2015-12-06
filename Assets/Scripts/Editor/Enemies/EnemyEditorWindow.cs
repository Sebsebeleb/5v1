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
            AttackBehaviour attack = prefab.GetComponent<AttackBehaviour>();
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

            SerializedProperty oldHealth = sDamage.FindProperty("BaseHealth");
            SerializedProperty attackProp = sAttack.FindProperty("StartingBaseAttack");
            SerializedProperty countdownProp = sCountdown.FindProperty("StartMaxCountdown");

            EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);

            oldHealth.intValue = EditorGUILayout.IntField("Base Health", oldHealth.intValue, EditorStyles.numberField);
            attackProp.intValue = EditorGUILayout.IntField("Base Attack", attackProp.intValue);
            countdownProp.intValue = EditorGUILayout.IntField("Countdown", countdownProp.intValue);

            sDamage.ApplyModifiedProperties();
            sAttack.ApplyModifiedProperties();
            sCountdown.ApplyModifiedProperties();
        }

    }
}