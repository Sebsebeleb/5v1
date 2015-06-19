using UnityEngine;

namespace Data.Skills
{
    class Stun : BaseSkill
    {
        public Stun()
        {
            SkillName = "Stun";
            Tooltip = "Stun an enemy for 5 turns";
            BaseCooldown = 11;

            CurrentCooldown = BaseCooldown;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

			Actor enemy = GridManager.TileMap.GetAt(x, y);
			
            enemy.GetComponent<EffectHolder>().AddEffect(new Stunned(5));
        }
        
        public override string GetName(){
            return SkillName;
        }
        
        public override string GetTooltip(){
            return Tooltip;
        }
    }
}
