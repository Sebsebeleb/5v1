using UnityEngine;

using Data.Effects;

namespace Data.Skills
{
    [System.Serializable]
    public class Blizzard : BaseSkill
    {

        public Blizzard(int PlayerLevel) : base(PlayerLevel)
        {
            SkillName = "Blizzard";
            Tooltip = "Deal {0} damage to all enemies. Then, if at least 4 enemies survive, stun them for {1} turn";
            BaseCooldown = 12;
            ManaCost = 25;
        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            // Deal damage
            foreach(var enemy in GridManager.TileMap.GetAll()){
                enemy.damagable.TakeDamage(getDamage());
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
            return 1 + Rank;
        }


        public override string GetTooltip()
        {
            string damageProp = TextUtilities.FontColor(Colors.DamageValue, getDamage().ToString());
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, "1");

            return string.Format(Tooltip, damageProp, durationProp);
        }
    }

}