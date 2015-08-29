using UnityEngine;

namespace Data.Skills
{
    [System.Serializable]
    class Stun : BaseSkill
    {
        public Stun(int PlayerLevel) : base(PlayerLevel)
        {
            SkillName = "Stun";
            Tooltip = "Stun an enemy for 5 turns";
            BaseCooldown = 11;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

			Actor enemy = GridManager.TileMap.GetAt(x, y);

            enemy.GetComponent<EffectHolder>().AddEffect(new Stunned(5));
        }


        public override string GetTooltip(){
            return Tooltip;
        }
    }
}
