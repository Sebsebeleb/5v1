using UnityEngine;

namespace Data.Skills
{
    [System.Serializable]
    class Switcheroo : BaseSkill
    {
		//TODO: Implement multitargeting and make this "Choose two enemies/corpses and swap their places. If both of them are alive, stun them for 2 turns"
        public Switcheroo()
        {
            SkillName = "Switcheroo";
            Tooltip = "Choose an enemy/corpse and swap it with the other enemy in it's column. If they are both enemies, also deal 1 damage and stun for 2 turns";
            BaseCooldown = 11;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

			Actor enemy = GridManager.TileMap.GetAt(x, y);

			int targetY = (y == 1 ? 0 : 1);
			Actor otherEnemy = GridManager.TileMap.GetAt(x, targetY);

			bool bothEnemies = (enemy.tag != "Corpse" && otherEnemy.tag != "Corpse" ? true : false);

			GridManager.TileMap.SwapActors(x, y, x, targetY);

			if (bothEnemies){
                enemy.damagable.TakeDamage(1);
                otherEnemy.damagable.TakeDamage(1);
                
	            enemy.effects.AddEffect(new Stunned(3));
				otherEnemy.effects.AddEffect(new Stunned(3));
			}

        }

        public override string GetName(){
            return SkillName;
        }

        public override string GetTooltip(){
            return Tooltip;
        }
    }
}
