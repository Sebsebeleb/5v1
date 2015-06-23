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
        GameObject newEnemy = RollEnemy(x, y);

        SpawnEnemy(newEnemy, x, y);
    }


    // Returns a higher and higher number as the bosscounter reaches 0
    private static int CalculateDifficultAdd(){
        return (int) ((80-TurnManager.BossCounter) * 0.20);
    }

    //Rolls a new enemy based on various factors.
    private static GameObject RollEnemy(EnemySpawnList spawnList, int x, int y)
    {
        int TotalChance = 0;
        
        // The difficult add is the number added to all rolls (to increase the chance of spawning rarer enemies as the game progresses
        int difficultyAdd = CalculateDifficultAdd();

        foreach (EnemySpawnList.EnemyEntry entry in spawnList.Entries)
        {
            // Ignore unspawnable
            if (!CanSpawnEnemyAt(entry, x, y)) continue;

            TotalChance += entry.SpawnChance + difficultyAdd;
            
        }

        int roll = Random.Range(0, TotalChance);

        int currentChanceCount = 0;
        foreach (EnemySpawnList.EnemyEntry entry in SpawnList.Entries)
        {
            // Ignore unspawnable
            if (!CanSpawnEnemyAt(entry, x, y)) continue;

            if (roll < currentChanceCount + entry.SpawnChance + difficultyAdd)
            {
                return entry.Enemy;
            }
            currentChanceCount += entry.SpawnChance + difficultyAdd;
        }
        Debug.LogError("Something went wrong with spawning. No enemy was chosen for some reason");
        return spawnList.Entries[0].Enemy;
    }


    // USe the currently selected spawn list
    private static GameObject RollEnemy(int x, int y)
    {
        return RollEnemy(SpawnList, x, y);
    }

    // Checks stuff like if the enemy is allowed to spawn in that row
    private static bool CanSpawnEnemyAt(EnemySpawnList.EnemyEntry entry, int x, int y)
    {
        if (y == 0)
        {
            return entry.BackRow;
        }
        if (y == 1)
        {
            return entry.FrontRow;
        }

        return true;
    }
}
