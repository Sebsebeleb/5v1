using UnityEngine;

namespace Data.Skills
{
    [System.Serializable]
    public class Bloodlust : BaseSkill
    {

        public Bloodlust(int PlayerLevel) : base(PlayerLevel)
        {
            SkillName = "Bloodlust";
            Tooltip = "Deal 200% damage to an enemy. If this attack kills it, heal life equal to twice the enemy's hp (from before the attack)";
            BaseCooldown = 10;

        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            Actor targetedEnemy = GridManager.TileMap.GetAt(x, y);

            int healAmount = targetedEnemy.damagable.CurrentHealth * 2;

            //TODO: This currently assumes the player is casting it. Bad?
            GameObject player = GameObject.FindWithTag("Player");
            AttackBehaviour playerAttack = player.GetComponent<AttackBehaviour>();
            Actor playerActor = player.GetComponent<Actor>();


            int tempBonus = playerAttack.CalculateAttack();
            playerAttack.BonusAttack += tempBonus;
            playerAttack.DoAttack(targetedEnemy);
            playerAttack.BonusAttack -= tempBonus;

            //TODO: Might be a bad way to find out if an enemy is dead. We'll find out!
            if (!targetedEnemy || targetedEnemy.damagable.CurrentHealth <= 0) {
                playerActor.damagable.Heal(healAmount);
            }
        }



        public override string GetTooltip(){
            return Tooltip;
        }
    }
}
