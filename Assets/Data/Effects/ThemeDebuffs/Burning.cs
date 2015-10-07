using UnityEngine;

using BaseClasses;

namespace Data.Effects
{
    public class Burning : Effect
    {

        public Burning(int duration) : base(duration)
        {
            IsDebuff = true;
            IsThemeDebuff = true;

            Description = new EffectDescription("Burning", () =>
            string.Format(
                "This enemy is burning, it takes {0} damage every turn",
                TextUtilities.FontColor(Colors.DamageValue, "1")
             ));
        }


        public override void OnTurn(){
            Burn();
        }

        private void Burn()
        {
            owner.damagable.TakeDamage(1);
        }

        public override bool ShouldAnimate()
        {
            return true;
        }

        public override ChangeAnimation GetAnimationInfo()
        {
            ChangeAnimation a = new ChangeAnimation();
            a.SpawnHoverText = true;
            a.IconName = "Burning";

            return a;
        }
    }
}