using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

// Middle-man class for loading various game assets
public static class GameResources
{

    private const string SearchPath = "Enemies";


    // Cache for GetEnemyByID. Possible TODO: Have this filled out at build-time by the editor
    private static Dictionary<int, GameObject> _enemyByIDCache = new Dictionary<int, GameObject>();
    public static GameObject GetEnemyByID(int id)
    {
        if (_enemyByIDCache.ContainsKey(id))
        {
            return _enemyByIDCache[id];
        }

        // Try to find the enemytype using Resources.Load
        GameObject enemy = _findEnemy(id);

        if (enemy == null)
        {
            Debug.LogError(new Exception("Error loading EnemyType with id: " + id));
            return null;
        }

        // Store it in the cache
        _enemyByIDCache[id] = enemy;

        return enemy;
    }

    // Looks through all gameobjects untill it finds one that has the Actor component and the correct ID
    private static GameObject _findEnemy(int id)
    {
        foreach (GameObject go in Resources.LoadAll<GameObject>(SearchPath))
        {
            Debug.Log("Hello, looking at: "+go.name);
            Actor act = go.GetComponent<Actor>();

            // If it doesn't have an actor component, it isnt an enemy type
            if (act == null)
            {
                continue;
            }

            if (act.enemyTypeID == id)
            {
                return go;
            }
        }

        return null;
    }
}