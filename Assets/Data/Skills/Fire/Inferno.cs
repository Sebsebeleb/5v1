using UnityEngine;
using Data.Effects;

namespace Data.Skills
{
    [System.Serializable]
    public class Inferno : BaseSkill
    {

        public Inferno(int PlayerLevel) : base(PlayerLevel)
        {
            SkillName = "Inferno";
            Tooltip = "Deal {0} damage to all enemies and apply burning for {1} turns";
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
            foreach(var enemy in enemies){
                if (enemy.tag != "Corpse" && enemy.damagable.CurrentHealth > 0){
                    enemy.effects.AddEffect(new Burning(getDuration()));
                }
            }
        }

        private int getDamage(){
            return 2 + Rank;
        }

        private int getDuration(){
            return 3;
        }


        public override string GetTooltip()
        {
            string damageProp = TextUtilities.FontColor(Colors.DamageValue, getDamage());
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, getDuration());

            return string.Format(Tooltip, damageProp, durationProp);
        }
    }

}