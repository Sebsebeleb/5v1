using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;

public static class EnemyManager
{

    public static GameObject CorpseEnemy;
	

    public static void KillEnemy(Enemy enemy)
    {
        //Make a new corpse enemy
        SpawnEnemy(CorpseEnemy, enemy.x, enemy.y);

        GameObject.Destroy(enemy);
    }

    public static void SpawnEnemy(GameObject enemy, int x, int y)
    {
	        GameObject newEnemy = GameObject.Instantiate(enemy.gameObject);

	        Enemy enemyBehaviour = newEnemy.GetComponent<Enemy>();
	        GridManager.TileMap.EnemySetAt(x, y, enemyBehaviour);

	        enemyBehaviour.x = x;
	        enemyBehaviour.y = y;
        
    }
}
