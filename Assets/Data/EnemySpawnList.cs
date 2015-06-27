using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Simple list of enemies that can spawn
/// </summary>
[Serializable]
public class EnemySpawnList : ScriptableObject
{
    [SerializeField]
    public List<EnemyEntry> Entries = new List<EnemyEntry>();
    [SerializeField]
    public GameObject Boss;

    [Serializable]
    public class EnemyEntry
    {
        public GameObject Enemy;
        public bool FrontRow = true;
        public bool BackRow = true;
        public int SpawnChance;
    }
}
