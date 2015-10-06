
// A simple generic attack buff
using BaseClasses;

namespace Data.Effects
{
[
    System.Serializable]
    class Stunned : Effect
    {


        public Stunned(int duration) : base(duration)
        {
            Description = new EffectDescription("Stunned",
                describe);
        }

        private string describe()
        {
            return "This enemy is stunned, it does not have it's countdown reduced";
        }

        public override void OnAdded()
        {
            base.OnAdded();

            // If wet, stun duration is increased by one
            if (owner.effects.HasEffect<Wet>()){
                Duration++;
                owner.effects.RemoveEffect(owner.effects.GetEffectsOfType<Wet>()[0]);
            }

            owner.status.SetStunned(true);
        }

        public override void OnRemoved()
        {
            base.OnRemoved();

            owner.status.SetStunned(false);
        }
    }
}