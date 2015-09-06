using Map;
using System.Collections.Generic;

namespace Data.Skills
{
    [System.Serializable]
    public class Cleave : BaseSkill
    {
        private int damage = 8;

        public Cleave(int PlayerLevel) : base(PlayerLevel)
        {
            SkillName = "Cleave";
            Tooltip = "Deal {0} damage to target and one random adjacent enemy";
            BaseCooldown = 12;
        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            // All of our final targets
            List<Actor> finalTargets = new List<Actor>();
            finalTargets.Add(GridManager.TileMap.GetAt(x, y));

            // The possible candidates for the "secondary adjacent enemy" part
            List<Actor> validSecondaryTargets = new List<Actor>();

            List<Actor> adjacent = GridManager.TileMap.GetAdjacent(x, y, AdjacancyType.Ortho);

            foreach (Actor enemy in adjacent)
            {
                if (enemy.tag != "Corpse")
                {
                    validSecondaryTargets.Add(enemy);
                }
            }

            // If any legal targets left, select a random one
            if (validSecondaryTargets.Count > 0)
            {
                validSecondaryTargets.Shuffle();
                finalTargets.Add(validSecondaryTargets[0]);
            }

            foreach (Actor enemy in finalTargets)
            {
                enemy.damagable.TakeDamage(getDamage());
            }

        }

        private int getDamage(){
           return 2 + Rank*3;
        }



        public override string GetTooltip(){
            string damageProp = TextUtilities.FontColor(Colors.DamageValue, getDamage().ToString());

            return string.Format(Tooltip, damageProp);
        }
    }

}
