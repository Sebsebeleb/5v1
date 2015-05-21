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

    public override void OnInspectorGUI()
    {
        EnemySpawnList list = target as EnemySpawnList;

        serializedObject.Update();
        var myIterator = serializedObject.FindProperty("possible");

        GUILayout.Label("hello");
        while (true) {
            var myRect = GUILayoutUtility.GetRect(0f, 16f);
            var showChildren = EditorGUI.PropertyField(myRect, myIterator);
            if (!myIterator.NextVisible(showChildren)) {
                break;
            }
        }

        serializedObject.ApplyModifiedProperties();

        EditorUtility.SetDirty(list);
    }

}
