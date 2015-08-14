using UnityEngine;

namespace Data.Skills
{
    [System.Serializable]
    public class Blizzard : BaseSkill
    {

        public Blizzard()
        {
            SkillName = "Blizzard";
            Tooltip = "Deal 2 damage to all enemies. Then, if at least 4 enemies survive, stun them for 1 turn";
            BaseCooldown = 12;
        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            // Deal damage
			foreach(var enemy in GridManager.TileMap.GetAll()){
				enemy.damagable.TakeDamage(2);
			}

            // Check if 4+ alive
			var enemies = GridManager.TileMap.GetAll();
			int numAlive = 0;
			foreach(var enemy in enemies){
				if (enemy.tag != "Corpse" && enemy.damagable.CurrentHealth > 0){
					numAlive++;
				}
			}

            Debug.Log("Num alive: " + numAlive);
            // If so, stun them
			if (numAlive >= 4){
				foreach(var enemy in enemies){
					enemy.effects.AddEffect(new Stunned(2));
				}
			}
        }

    public override string GetName()
        {
            return SkillName;
        }

        public override string GetTooltip()
        {
            return Tooltip;
        }
    }

}