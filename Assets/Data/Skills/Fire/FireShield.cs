namespace BBG.Data.Skills.Fire
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects;

    using UnityEngine;

    [System.Serializable]
    class FireShield : BaseSkill
    {
        public FireShield(int PlayerLevel) : base(PlayerLevel)
        {
            this.Category = SkillCategory.Fire;

            this.SkillName = "Fire Shield";
            this.Tooltip = "For the next {0} turns, you will deal {1} damage to all attackers and apply burning to them. Enemies that are already burning take an extra {2} damage";
            this.BaseCooldown = 18;
            this.ManaCost = 20;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            GameObject player = GameObject.FindWithTag("Player");

            player.GetComponent<EffectHolder>().AddEffect(new FireShieldEffect(this.getDuration(), this.getDamage(), this.getBonusDamage()));
        }

        private int getDuration(){
            return 3 + (int) this.Rank/3;
        }

        private int getDamage(){
            return 1 + (int) this.Rank/2;
        }

        private int getBonusDamage(){
            return this.Rank;
        }


        public override string GetTooltip(){
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, this.getDuration().ToString());
            string retalitateProp = TextUtilities.FontColor(Colors.DamageValue, this.getDamage());
            string bonusProp = TextUtilities.FontColor(Colors.DamageValue, this.getBonusDamage());

            return string.Format(this.Tooltip, durationProp, retalitateProp, bonusProp);
        }
    }
}
