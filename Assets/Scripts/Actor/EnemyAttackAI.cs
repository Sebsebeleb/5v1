using UnityEngine;

namespace BBG.Actor
{
    public class EnemyAttackAI : MonoBehaviour
    {

        private AI _brain;
        private Actor actor;

        private GameObject player;
        private Damagable playerDamage;

        void Awake()
        {
            this.player = GameObject.FindWithTag("Player");
            this.playerDamage = this.player.GetComponent<Damagable>();

            this._brain = this.GetComponent<AI>();
            this.actor = this.GetComponent<Actor>();
        }

        void Start()
        {
            AI.AiAction attackAction = new AI.AiAction();
            attackAction.Name = "Attack";
            attackAction.AnimationName = "Attack";
            attackAction.Description = this.GetDescription;
            attackAction.Callback = this.AttackPlayer;
            attackAction.CalcPriority = this.CalculatePriority;
            attackAction.IsFreeAction = false;

            this._brain.AddAction(attackAction);
        }

        private string GetDescription(){
            int damage = this.actor.attack.Attack;
            string damageText = TextUtilities.Bold(TextUtilities.FontColor("#FF2222", damage.ToString()));
            return "Attacks you for " + TextUtilities.FontColor("#FF2222", damageText) + " damage";
        }


        private int CalculatePriority()
        {
            return 50;
        }

        private void AttackPlayer()
        {
            // Argument for the event
            EnemyAttackArgs args;
            args.who = this.actor;
            args.rawDamage = this.actor.attack.Attack;

            // Deal damage
            this.playerDamage.TakeDamage(this.actor.attack.Attack);

            // Fire off the event
            EventManager.Notify(Events.EnemyAttack, args);
        }
    }
}
