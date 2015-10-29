using UnityEngine;

namespace Data.Skills
{
    [System.Serializable]
    class FireShield : BaseSkill
    {
        public FireShield(int PlayerLevel) : base(PlayerLevel)
        {
            SkillName = "Fire Shield";
            Tooltip = "For the next {0} turns, you will deal {1} damage to all attackers and apply burning to them. Enemies that are already burning take an extra {2} damage";
            BaseCooldown = 18;
            ManaCost = 20;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            GameObject player = GameObject.FindWithTag("Player");

            player.GetComponent<EffectHolder>().AddEffect(new FireShieldEffect(getDuration(), getDamage(), getBonusDamage()));
        }

        private int getDuration(){
            return 3 + (int) Rank/3;
        }

        private int getDamage(){
            return 1 + (int) Rank/2;
        }

        private int getBonusDamage(){
            return Rank;
        }


        public override string GetTooltip(){
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, getDuration().ToString());
            string retalitateProp = TextUtilities.FontColor(Colors.DamageValue, getDamage());
            string bonusProp = TextUtilities.FontColor(Colors.DamageValue, getBonusDamage());

            return string.Format(Tooltip, durationProp, retalitateProp, bonusProp);
        }
    }
}
