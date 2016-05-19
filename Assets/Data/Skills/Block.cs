namespace BBG.Data.Skills
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects;

    using UnityEngine;

    [System.Serializable]
    class Block : BaseSkill
    {
        public Block(int PlayerLevel) : base(PlayerLevel)
        {
            this.Category = SkillCategory.Warrior;
            
            this.SkillName = "Block";
            this.Tooltip = "Block all damage that would be dealt for {0} turns";
            this.BaseCooldown = 18;
            this.ManaCost = 15;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            GameObject player = GameObject.FindWithTag("Player");

            player.GetComponent<EffectHolder>().AddEffect(new Blocking(2));
        }

        private int getDuration(){
            return 1 + this.Rank;
        }


        public override string GetTooltip(){
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, this.getDuration().ToString());

            return string.Format(this.Tooltip, durationProp);
        }
    }
}
