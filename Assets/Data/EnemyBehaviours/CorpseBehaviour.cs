using UnityEngine;
using System.Collections;

public class CorpseBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void DoRespawn()
    {
        Enemy enemy = GetComponent<Enemy>();
        EnemyManager.SpawnRandomEnemy(enemy.x, enemy.y);
    }
}
