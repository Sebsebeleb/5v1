namespace BBG.Data.Effects.ThemeDebuffs
{
    using BBG.BaseClasses;
    using BBG.View;

    public class Burning : Effect
    {

        public Burning(int duration) : base(duration)
        {
            this.IsDebuff = true;
            this.IsThemeDebuff = true;

            this.Description = new EffectDescription("Burning", () =>
            string.Format(
                "This enemy is burning, it takes {0} damage every turn",
                TextUtilities.FontColor(Colors.DamageValue, "1")
             ));
        }


        public override void OnTurn(){
            this.Burn();
        }

        private void Burn()
        {
            this.owner.damagable.TakeDamage(1);
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