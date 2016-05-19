namespace BBG.Data.Skills
{
    using BBG.Actor;
    using BBG.BaseClasses;

    using UnityEngine;

    [System.Serializable]
    public class Bloodlust : BaseSkill
    {

        public Bloodlust(int PlayerLevel) : base(PlayerLevel)
        {
            this.Category = SkillCategory.Warrior;

            this.SkillName = "Bloodlust";
            this.Tooltip = "Deal {0} damage to an enemy. If this attack kills it, heal life equal to twice the enemy's hp (from before the attack)";
            this.BaseCooldown = 10;
            this.ManaCost = 10;

        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            Actor targetedEnemy = GridManager.TileMap.GetAt(x, y);

            int healAmount = targetedEnemy.damagable.CurrentHealth * 2;

            //TODO: This currently assumes the player is casting it. Bad?
            GameObject player = GameObject.FindWithTag("Player");
            ActorAttack playerAttack = player.GetComponent<ActorAttack>();
            Actor playerActor = player.GetComponent<Actor>();


            int tempBonus = playerAttack.CalculateAttack() * ((int) this.getDamageMultiplier() - 1);
            playerAttack.BonusAttack += tempBonus;
            playerAttack.DoAttack(targetedEnemy);
            playerAttack.BonusAttack -= tempBonus;

            //TODO: Might be a bad way to find out if an enemy is dead. We'll find out!
            if (!targetedEnemy || targetedEnemy.damagable.CurrentHealth <= 0) {
                playerActor.damagable.Heal(healAmount);
            }
        }

        private float getDamageMultiplier(){
            return 1.5f+this.Rank*0.5f;
        }

        public override string GetTooltip(){
            string damageMultProp = TextUtilities.FontColor(Colors.DamageValue, this.getDamageMultiplier().ToString() + "x");

            return string.Format(this.Tooltip, damageMultProp);
        }
    }
}
