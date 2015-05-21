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
    public List<GameObject> possible = new List<GameObject>();
}
