
namespace BBG.Data.Skills.Ice
{
    using System.Collections.Generic;

    using BBG.BaseClasses;
    using BBG.Data.Effects;
    using BBG.Map;

    [System.Serializable]
    public class Blizzard : BaseSkill
    {

        public Blizzard(int PlayerLevel) : base(PlayerLevel)
        {
            this.Category = SkillCategory.Water;

            this.SkillName = "Blizzard";
            this.Tooltip = "Deal {0} damage to all enemies. Then, if at least 4 enemies survive, stun them for {1} turn";
            this.BaseCooldown = 12;
            this.ManaCost = 20;
        }

        public override List<GridPosition> GetAffectedTargets(GridPosition target)
        {
            return Targeting.Targets.NotCorpses();
        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            // Deal damage
            foreach(var enemy in GridManager.TileMap.GetAll()){
                enemy.damagable.TakeDamage(this.getDamage());
            }

            // Check if 4+ alive
            var enemies = GridManager.TileMap.GetAll();
            int numAlive = 0;
            foreach(var enemy in enemies){
                if (enemy.tag != "Corpse" && enemy.damagable.CurrentHealth > 0){
                    numAlive++;
                }
            }

            // If so, stun them
            if (numAlive >= 4){
                foreach(var enemy in enemies){
                    enemy.effects.AddEffect(new Stunned(2));
                }
            }
        }

        private int getDamage(){
            return 1 + this.Rank;
        }


        public override string GetTooltip()
        {
            string damageProp = TextUtilities.FontColor(Colors.DamageValue, this.getDamage().ToString());
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, "1");

            return string.Format(this.Tooltip, damageProp, durationProp);
        }
    }

}