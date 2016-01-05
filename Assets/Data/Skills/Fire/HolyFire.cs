using Data.Effects;
using System.Collections.Generic;

namespace Data.Skills
{

    using Map;
    using System.Linq;

    class HolyFire : BaseSkill
    {
        public HolyFire(int playerLevel) : base(playerLevel)
        {
            Category = SkillCategory.Fire;

            SkillName = "Holy Fire";
            Tooltip = "Deal {0} damage to an enemy and all adjacent enemies and apply burning ({1}) to them";
            BaseCooldown = 10;
            ManaCost = 15;
        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            List<Actor> targets = new List<Actor>();

            targets.Add(GridManager.TileMap.GetAt(x, y));
            targets.AddRange(GridManager.TileMap.GetAdjacent(x, y));

            int damage = getDamage();
            int duration = getBurningDuration();

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
            return 2 + Rank;
        }

        private int getBurningDuration()
        {
            return 3;
        }

        public override string GetTooltip()
        {
            string damageProp = TextUtilities.FontColor(Colors.DamageValue, getDamage().ToString());
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, getBurningDuration().ToString());

            return string.Format(Tooltip, damageProp, durationProp);
        }
    }
}
