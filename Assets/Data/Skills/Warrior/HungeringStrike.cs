﻿namespace Data.Skills.Warrior
{
    using Map;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Deals x damage (based on weapon damage) to enemy. If this attack kills the enemy, deal x * 1.25 damage to a random adjacent enemy. Repeat up to 6 times.
    /// </summary>
    public class HungeringStrike : BaseSkill
    {
        public HungeringStrike(int PlayerLevel)
            : base(PlayerLevel)
        {
            Category =SkillCategory.Warrior;


            SkillName = "Hungering Strike";
            Tooltip = "blah blah";

            BaseCooldown = 10;
            ManaCost = 15;

        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            Actor target = GridManager.TileMap.GetAt(x, y);

            int i = 0;
            while (target != null && i < 6)
            {
                target.damagable.TakeDamage(this.GetDamage() + 2 * i);

                if (target.damagable.Dead)
                {
                    var newTargets = GridManager.TileMap.GetAdjacent(target.x, target.y).Where(actor => !actor.damagable.Dead).ToList();

                    newTargets.Shuffle();

                    target = newTargets[0];
                }

                i++;
            }
        }

        private int GetDamage()
        {
            return Actor.Player.GetWeaponDamage(0.5f, 2);
        }

        public override List<GridPosition> GetAffectedTargets(GridPosition target)
        {
            return Targeting.Targets.AffectedSingleTarget(target).Concat( Targeting.Targets.Adjacent(target)).ToList();
        }

        public override string GetTooltip()
        {
            return string.Format(this.Tooltip);
        }
    }
}