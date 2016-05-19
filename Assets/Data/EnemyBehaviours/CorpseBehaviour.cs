using UnityEngine;

namespace BBG.Data.EnemyBehaviours
{
    using BBG.Actor;

    public class CorpseBehaviour : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            // Should be the only action the "ai" can do, which is to spawn a new enemy
            AI.AiAction respawnAction = new AI.AiAction();
            respawnAction.Name = "Reincarnate";
            respawnAction.AnimationName = "Attack";
            respawnAction.Description = () => "Spawns a new creature";
            respawnAction.CalcPriority = () => 10000;
            respawnAction.Callback = this.DoRespawn;
            respawnAction.IsFreeAction = false;
        
        
            this.GetComponent<AI>().AddAction(respawnAction);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void DoRespawn()
        {
            Actor actor = this.GetComponent<Actor>();
            EnemyManager.SpawnRandomEnemy(actor.x, actor.y);
        }
    }
}
