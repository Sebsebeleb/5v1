using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class EnemyManager
{

    public static GameObject CorpseEnemy;

    public static EnemySpawnList SpawnList;


    public static void KillEnemy(Actor actor)
    {

        int x = actor.x;
        int y = actor.y;


        //Make a new corpse enemy
        SpawnEnemy(CorpseEnemy, x, y);

    }

    public static void SpawnEnemy(GameObject enemy, int x, int y)
    {
        CheckCurrent(enemy, x, y);

        GameObject newEnemy = GameObject.Instantiate(enemy);

        Actor actorBehaviour = newEnemy.GetComponent<Actor>();
        GridManager.TileMap.EnemySetAt(x, y, actorBehaviour);

        actorBehaviour.x = x;
        actorBehaviour.y = y;
    }

    /// <summary>
    /// Makes a check on the current enemy in the grid
    /// </summary>
    private static void CheckCurrent(GameObject toSpawn, int x, int y)
    {
        Actor actor = GridManager.TileMap.GetAt(x, y);
        if (actor != null)
        {

            //A corpse is ok, just remove it and go on. Its also ok to spawn a corpse
            if (actor.tag == "Corpse" || toSpawn.tag == "Corpse")
            {
                GameObject.Destroy(actor.gameObject);
            }
            else
            {
                // Something is trying to spawn an enemy on top of an exisiting enemy.
                throw new Exception("SpawnOnTopOfExisitingEnemyError");
            }
        }
    }

    public static void SpawnRandomEnemy(int x, int y)
    {
        GameObject newEnemy = RollEnemy();

        SpawnEnemy(newEnemy, x, y);
    }



    //Rolls a new enemy based on various factors.
    private static GameObject RollEnemy(EnemySpawnList spawnList)
    {
        int max = spawnList.possible.Count;
        int roll = Random.Range(0, max);

        GameObject rolled = spawnList.possible[roll];

        return rolled;
    }

    // USe the currently selected spawn list
    private static GameObject RollEnemy()
    {
        return RollEnemy(SpawnList);
    }
}
