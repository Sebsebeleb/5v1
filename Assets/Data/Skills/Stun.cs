
namespace BBG.Data.Skills
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects;

    [System.Serializable]
    class Stun : BaseSkill
    {
        public Stun(int PlayerLevel) : base(PlayerLevel)
        {
            this.Category = SkillCategory.Warrior;

            this.SkillName = "Stun";
            this.Tooltip = "Stun an enemy for {0} turns";
            this.BaseCooldown = 11;
            this.ManaCost = 5;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            Actor enemy = GridManager.TileMap.GetAt(x, y);

            enemy.GetComponent<EffectHolder>().AddEffect(new Stunned(this.getDuration()));
        }

        private int getDuration(){
            return 4+ (int)(this.Rank*1.5f);
        }


        public override string GetTooltip(){
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, this.getDuration().ToString());

            return string.Format(this.Tooltip, durationProp);
        }
    }
}
