using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public Enemy[] startingEnemies = new Enemy[6];

	// Use this for initialization
	void Start () {
	    for (int i = 0; i <= 5; i++) {
	        if (startingEnemies[i] == null) {
	            continue;
	        }
	        GameObject newEnemy = Instantiate(startingEnemies[i].gameObject);
	        GridManager.TileMap.EnemySetAt(i, newEnemy.GetComponent<Enemy>());
	    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
