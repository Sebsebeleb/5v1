using UnityEngine;

namespace Data.Skills
{
    [System.Serializable]
    class Stun : BaseSkill
    {
        public Stun(int PlayerLevel) : base(PlayerLevel)
        {
            SkillName = "Stun";
            Tooltip = "Stun an enemy for {0} turns";
            BaseCooldown = 11;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

			Actor enemy = GridManager.TileMap.GetAt(x, y);

            enemy.GetComponent<EffectHolder>().AddEffect(new Stunned(getDuration()));
        }

        private int getDuration(){
            return 4+ (int)(Rank*1.5f);
        }


        public override string GetTooltip(){
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, getDuration().ToString());

            return string.Format(Tooltip, durationProp);
        }
    }
}
