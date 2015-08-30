using UnityEngine;

namespace Data.Skills
{
    [System.Serializable]
    class Block : BaseSkill
    {
        public Block(int PlayerLevel) : base(PlayerLevel)
        {
            SkillName = "Block";
            Tooltip = "Block all damage that would be dealt for {0} turns";
            BaseCooldown = 18;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            GameObject player = GameObject.FindWithTag("Player");

            player.GetComponent<EffectHolder>().AddEffect(new Blocking(2));
        }

        private int getDuration(){
            return 1 + Rank;
        }


        public override string GetTooltip(){
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, getDuration().ToString());

            return string.Format(Tooltip, durationProp);
        }
    }
}
