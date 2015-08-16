using UnityEngine;

namespace Data.Skills
{
    [System.Serializable]
    class LightningBolt : BaseSkill
    {
        public LightningBolt()
        {
            SkillName = "Lightning Bolt";
            Tooltip = "Deal 4 damage to an enemy and make it electrified. If it was already electrified,remove electrified and deal 1 damage to all other enemies.";
            BaseCooldown = 7;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

			Actor target = GridManager.TileMap.GetAt(x, y);

			target.damagable.TakeDamage(4);

            if (target.effects.HasEffect<Data.Effects.Electrified>()){
				//TODO: Improve this api. This is pretty silly
				var f = target.effects.GetEffectsOfType<Data.Effects.Electrified>()[0];

				target.effects.RemoveEffect(f);
				foreach(Actor enemy in GridManager.TileMap.GetAll()){
					if (enemy.tag != "Corpse" && enemy != target){
						enemy.damagable.TakeDamage(1);
					}
				}
			}
			else{
				target.effects.AddEffect(new Data.Effects.Electrified());
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
