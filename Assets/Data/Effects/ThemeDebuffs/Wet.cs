using UnityEngine;

using BaseClasses;

namespace Data.Effects
{
    // Actual effect is done in stun
    public class Wet : Effect
    {

        public Wet(int duration) : base(duration)
        {
            IsDebuff = true;
            IsThemeDebuff = true;

            Description = new EffectDescription("Wet", () =>
                "The next time this enemy is stunned, it will be stunned an additional turn and consume wet"
             );
        }

        public override bool ShouldAnimate()
        {
            return true;
        }

        public override ChangeAnimation GetAnimationInfo()
        {
            ChangeAnimation a = new ChangeAnimation();
            a.SpawnHoverText = true;
            a.IconName = "Wet";

            return a;
        }
    }
}