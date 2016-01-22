using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawnList))]
public class EnemySpawnListEditor : Editor
{

    [MenuItem("Assets/Create/EnemySpawnList")]
    public static void MakeSpawnList()
    {
        Object obj = ScriptableObject.CreateInstance<EnemySpawnList>();
        AssetDatabase.CreateAsset(obj, "Assets/NewSpawnList.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

    }

    // TODO: FIXME: This should not be commited.
    public override void OnInspectorGUI()
    {
        EnemySpawnList list = target as EnemySpawnList;

        serializedObject.Update();
        var myIterator = serializedObject.FindProperty("Entries");

        while (true)
        {
            //EditorGUILayout.Separator();
            //GUILayoutUtility.BeginGroup("BaseItem");
            var showChildren = EditorGUILayout.PropertyField(myIterator, true);
            if (!myIterator.NextVisible(showChildren))
            {
                break;
            }

            //EditorGUILayout.IntField("Test", 2);
            //GUILayoutUtility.EndGroup("BaseItem");
        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(list);
    }

    /*public override void OnInspectorGUI()
    {
        EnemySpawnList list = target as EnemySpawnList;

        serializedObject.Update();

        var enemies = serializedObject.FindProperty("possible");

        enemies.
    }*/
}
