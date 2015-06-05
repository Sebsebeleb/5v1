using UnityEngine;

public class CorpseBehaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // Should be the only action the "ai" can do, which is to spawn a new enemy
        GetComponent<AI>().AddAction(() => new AI.ActionPriority(100, DoRespawn));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DoRespawn()
    {
        Actor actor = GetComponent<Actor>();
        EnemyManager.SpawnRandomEnemy(actor.x, actor.y);
    }
}
