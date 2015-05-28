using UnityEngine;

namespace Data.Skills
{
    class Block : BaseSkill
    {
        public Block()
        {
            SkillName = "Block";
            Tooltip = "Block all damage that would be dealt for two turns";
            BaseCooldown = 18;

            CurrentCooldown = BaseCooldown;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            GameObject player = GameObject.FindWithTag("Player");

            player.GetComponent<EffectHolder>().AddEffect(new Blocking(2));
        }
    }
}
