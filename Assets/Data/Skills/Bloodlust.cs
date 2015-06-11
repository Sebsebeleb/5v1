using UnityEngine;

namespace Data.Skills
{
    public class Bloodlust : BaseSkill
    {

        public Bloodlust()
        {
            SkillName = "Bloodlust";
            Tooltip = "Deal 150% damage to an enemy. If this attack kills it, gain life equal to the enemy's hp";
            BaseCooldown = 12;

        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            Actor targetedEnemy = GridManager.TileMap.GetAt(x, y);

            int healAmount = targetedEnemy.damagable.CurrentHealth;

            //TODO: This currently assumes the player is casting it. Bad?
            GameObject player = GameObject.FindWithTag("Player");
            AttackBehaviour playerAttack = player.GetComponent<AttackBehaviour>();
            Actor playerActor = player.GetComponent<Actor>();


            int tempBonus = playerAttack.CalculateAttack() / 2;
            playerAttack.BonusAttack += tempBonus;
            playerAttack.DoAttack(targetedEnemy);
            playerAttack.BonusAttack -= tempBonus;

            //TODO: Might be a bad way to find out if an enemy is dead. We'll find out!
            if (!targetedEnemy || targetedEnemy.damagable.CurrentHealth <= 0) {
                playerActor.damagable.Heal(healAmount);
            }
        }
        
        public override string GetName(){
            return SkillName;
        }
        
        public override string GetTooltip(){
            return Tooltip;
        }
    }
}
