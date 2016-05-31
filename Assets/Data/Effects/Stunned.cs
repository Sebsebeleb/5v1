
// A simple generic attack buff

namespace BBG.Data.Effects
{
    using BBG.BaseClasses;
    using BBG.Data.Effects.ThemeDebuffs;

    [System.Serializable]
    class Stunned : Effect
    {


        public Stunned(int duration) : base(duration)
        {
            this.IsDebuff = true;

            this.Description = new EffectDescription("Stunned",
                this.describe);
        }

        private string describe()
        {
            return "This enemy is stunned, it does not have it's countdown reduced";
        }

        public override void OnAdded()
        {
            base.OnAdded();

            // If wet, stun duration is increased by one
            if (this.owner.effects.HasEffect<Wet>())
            {
                this.Duration++;
                this.owner.effects.RemoveEffect(this.owner.effects.GetEffectsOfType<Wet>()[0]);
            }

            this.owner.status.SetStunned(true);
        }

        public override void OnRemoved()
        {
            base.OnRemoved();

            this.owner.status.SetStunned(false);
        }
    }
}