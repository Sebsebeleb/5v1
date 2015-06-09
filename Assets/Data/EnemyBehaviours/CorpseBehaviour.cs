using UnityEngine;

public class CorpseBehaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        // Should be the only action the "ai" can do, which is to spawn a new enemy
        AI.AiAction respawnAction = new AI.AiAction();
        respawnAction.Name = "Reincarnate";
        respawnAction.Description = () => "Spawns a new creature";
        respawnAction.CalcPriority = () => 10000;
        respawnAction.Callback = DoRespawn;
        respawnAction.IsFreeAction = false;
        
        GetComponent<AI>().AddAction(respawnAction);
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
