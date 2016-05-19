namespace BBG.Data.Skills.Fire
{
    using System.Collections.Generic;
    using System.Linq;

    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects.ThemeDebuffs;
    using BBG.Map;

    [System.Serializable]
    public class WildFire : BaseSkill
    {

        // Sort of like arcane missiles in HS. But the animation should NOT take as long so ideas:
        // Have each missile deal more damage as the damage is higher
        // Have the number of missiles not scale very high, but the damage by each scale
        // OR:
        // Just have one missile per enemy hit, but have each missile deal the randomly allocated damage
        public WildFire(int PlayerLevel) : base(PlayerLevel)
        {
            this.Category = SkillCategory.Fire;

            this.SkillName = "Wildfire";
            this.Tooltip = "Deal {0} damage randomly split among all burning enemies. Damage is increased by {1} per burning enemy.";
            // Alternatively: Deal {0} damage randomly split among all enemies. Burning enemies take x more damage. All enemies that were hit start burning
            this.BaseCooldown = 12;
            this.ManaCost = 10;
        }

        // Can only be used if there actually is a burning enemy
        public override bool CanUse(int x, int y){
            if (!base.CanUse(x, y)){
                return false;
            }

            foreach(Actor enemy in GridManager.TileMap.GetAll()){
                if (enemy.effects.HasEffect<Burning>()){
                    return true;
                }
            }

            return false;
        }

        public override List<GridPosition> GetAffectedTargets(GridPosition target)
        {
            return Targeting.Targets.NotCorpses();
        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            // we create a list that contains the targets and how much damage they will take
            Dictionary<Actor, int> validTargets = new Dictionary<Actor, int>();

            foreach(Actor enemy in GridManager.TileMap.GetAll()){
                if (enemy.effects.HasEffect<Burning>()){
                    validTargets.Add(enemy, 0);
                }
            }

            // Find the number of burning enemies
            int totalBurningEnemies =
                (from enemy in GridManager.TileMap.GetAll()
                where enemy.effects.HasEffect<Burning>()
                select enemy)
                .Count();

            int totalDamage = this.getDamage() + this.getBonusDamage() * totalBurningEnemies;

            validTargets.ElementAt(0);

            //Now assign it randomly
            for (int i = totalDamage; i > 0; i--){
                var target = validTargets.RandomElement().Key;
                validTargets[target]++;
            }

            // And then deal it
            foreach (var pair in validTargets){
                pair.Key.damagable.TakeDamage(pair.Value);
            }
        }

        private int getDamage(){
            return 2 + this.Rank;
        }

        private int getBonusDamage(){
            return 1 + (int) this.Rank/2;
        }


        public override string GetTooltip()
        {
            string damageProp = TextUtilities.FontColor(Colors.DamageValue, this.getDamage());
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, this.getBonusDamage());

            return string.Format(this.Tooltip, damageProp, durationProp);
        }
    }

}