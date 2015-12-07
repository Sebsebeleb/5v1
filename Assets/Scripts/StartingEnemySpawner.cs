using UnityEngine;

public class StartingEnemySpawner : MonoBehaviour
{

    public GameObject[] startingEnemies = new GameObject[6];

    // The "enemy" that is spawned when an enemy dies. Pretty much only has countdown
    public GameObject CorpseEnemy;
    public EnemySpawnList InitialSpawnList;

    // Use this for initialization
    void Start()
    {
        // We said the global corpseenemy like this
        EnemyManager.CorpseEnemy = CorpseEnemy;

        EnemyManager.SpawnList = InitialSpawnList;

        for (int i = 0; i <= 5; i++)
        {
            if (startingEnemies[i] == null)
            {
                continue;
            }
            int x = i % 3;
            int y = i < 3 ? 0 : 1;

            EnemyManager.SpawnEnemy(startingEnemies[i], x, y);

        }
    }

    void Update()
    {

    }
}