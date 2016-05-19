namespace BBG.Data.Skills.Fire
{
    using System.Collections.Generic;
    using System.Linq;

    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects.ThemeDebuffs;
    using BBG.Map;

    class HolyFire : BaseSkill
    {
        public HolyFire(int playerLevel) : base(playerLevel)
        {
            this.Category = SkillCategory.Fire;

            this.SkillName = "Holy Fire";
            this.Tooltip = "Deal {0} damage to an enemy and all adjacent enemies and apply burning ({1}) to them";
            this.BaseCooldown = 10;
            this.ManaCost = 15;
        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            List<Actor> targets = new List<Actor>();

            targets.Add(GridManager.TileMap.GetAt(x, y));
            targets.AddRange(GridManager.TileMap.GetAdjacent(x, y));

            int damage = this.getDamage();
            int duration = this.getBurningDuration();

            foreach(Actor enemy in targets)
            {
                enemy.damagable.TakeDamage(damage);
                enemy.effects.AddEffect(new Burning(duration));
            }
        }

        public override List<GridPosition> GetAffectedTargets(GridPosition target)
        {
            return Targeting.Targets.AffectedSingleTarget(target).Concat( Targeting.Targets.Adjacent(target)).ToList();
        }

        private int getDamage()
        {
            return 2 + this.Rank;
        }

        private int getBurningDuration()
        {
            return 3;
        }

        public override string GetTooltip()
        {
            string damageProp = TextUtilities.FontColor(Colors.DamageValue, this.getDamage().ToString());
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, this.getBurningDuration().ToString());

            return string.Format(this.Tooltip, damageProp, durationProp);
        }
    }
}
