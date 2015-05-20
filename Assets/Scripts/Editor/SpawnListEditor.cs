using UnityEditor;
using UnityEngine;
using System.Collections;

public class SpawnListEditor : Editor {

    [MenuItem("MakeSpawnList")]
    public void MakeSpawnList()
    {
        ScriptableObject.CreateInstance<EnemySpawnList>();
    }
}
