using UnityEngine;
using System.Collections;

public class CorpseBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Should be the only action the "ai" can do, which is to spawn a new enemy
	    GetComponent<AI>().AddAction(() => new AI.ActionPriority(100, DoRespawn));
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
