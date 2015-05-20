using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Simple list of enemies that can spawn
/// </summary>
public class EnemySpawnList : ScriptableObject
{
    public List<GameObject> possible = new List<GameObject>();
}
