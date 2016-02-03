using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class EnemyManager
{

    public static GameObject CorpseEnemy;

    public static EnemySpawnList SpawnList;

    /// <summary>
    /// Kill an enemy, causing a corpse to take its place. 
    /// </summary>
    /// <param name="actor">Actor to kill</param>
    public static void KillEnemy(Actor actor)
    {

        int x = actor.x;
        int y = actor.y;


        //Make a new corpse enemy
        SpawnEnemy(CorpseEnemy, x, y);

    }

    /// <summary>
    /// Spawn a new enemy at specified position.
    /// </summary>
    /// <param name="enemy">Prefab of enemy to spawn</param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="rank"></param>
    /// <param name="InitializeStartEffects">If false, will not do initalization code. Mostly for deserialization</param>
    public static void SpawnEnemy(GameObject enemy, int x, int y, int rank, bool InitializeStartEffects = true)
    {
        CheckCurrent(enemy, x, y);

        GameObject newEnemy = GameObject.Instantiate(enemy);
        Actor actorBehaviour = newEnemy.GetComponent<Actor>();
        if (newEnemy.tag != "Corpse")
        {
            actorBehaviour.Rank = rank;
        }

        GridManager.TileMap.EnemySetAt(x, y, actorBehaviour);

        actorBehaviour.x = x;
        actorBehaviour.y = y;

        if (InitializeStartEffects)
        {
            Event.EventManager.Notify(Event.Events.PostEnemySpawned, actorBehaviour);

            newEnemy.BroadcastMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

/*            ApplyEffectsOnLoad startEffects = newEnemy.GetComponent<ApplyEffectsOnLoad>();
            if (startEffects != null)
            {
                startEffects.AddEffects();
            }*/
        }
    }

    public static void SpawnEnemy(GameObject enemy, int x, int y, bool InitializeStartEffects = true)
    {
        // TODO: Slow start
        int rank = (int)Math.Pow((CalculateDifficultAdd() / 5), 0.83f) + 1;
        
        SpawnEnemy(enemy, x, y, rank, InitializeStartEffects);
    }

    public static void SpawnBoss()
    {
        SpawnEnemy(SpawnList.Boss, 1, 0);
        SpawnRandomEnemy(0, 0);
        SpawnRandomEnemy(0, 1);
        SpawnRandomEnemy(2, 0);
        SpawnRandomEnemy(2, 1);
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
    private static int CalculateDifficultAdd()
    {
        return (int)(((Zone.Zone.current.ZoneLength - TurnManager.BossCounter) * 0.20) * Zone.Zone.current.EnemyDifficultyMod);
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
