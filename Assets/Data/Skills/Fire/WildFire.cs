using UnityEngine;
using Data.Effects;

namespace Data.Skills
{
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
            SkillName = "Wildfire";
            Tooltip = "Deal {0} damage randomly split among all burning enemies. Damage is increased by {1} per burning enemy.";
            // Alternatively: Deal {0} damage randomly split among all enemies. Burning enemies take x more damage. All enemies that were hit start burning
            BaseCooldown = 12;
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