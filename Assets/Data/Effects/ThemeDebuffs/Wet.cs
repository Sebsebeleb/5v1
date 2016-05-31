namespace BBG.Data.Effects.ThemeDebuffs
{
    using BBG.BaseClasses;
    using BBG.View;

    // Actual effect is done in stun
    [System.Serializable]
    public class Wet : Effect
    {

        public Wet(int duration) : base(duration)
        {
            this.IsDebuff = true;
            this.IsThemeDebuff = true;

            this.Description = new EffectDescription("Wet", () =>
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