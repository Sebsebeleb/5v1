using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Data.Effects;

namespace Data.Skills
{
    class HolyFire : BaseSkill
    {
        public HolyFire(int playerLevel) : base(playerLevel)
        {
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
