namespace BBG.Data.Effects.Specializations
{
    using BBG.Actor;
    using BBG.BaseClasses;

    public class Warlockery : Effect
    {
        public Warlockery()
            : base()
        {
            this.IsTrait = true;
            this.Description = new EffectDescription("Warlockery", this.Describe);
        }

        private string Describe()
        {
            return @"Gain focus on <color=""purple"">Warlockery</color> skills
Whenever a debuffed enemy dies, heal for 1% of your max health, +0.5% for each debuff it had.";
        }

        protected override void Created()
        {
            base.Created();

            ActorDied callback = this.OnDeath;
            EventManager.Register(Events.ActorDied, callback);
        }

        private void OnDeath(Actor who)
        {
            int debuffs = 0;

            foreach (Effect eff in who.effects)
            {
                if (eff.IsDebuff)
                {
                    debuffs++;
                }
            }

            if (debuffs >= 1)
            {
                Damagable dmg = this.owner.damagable;
                int heal = (int)(dmg.MaxHealth * (1f + 0.05f * debuffs));
                this.owner.damagable.Heal(heal);
            }
        }
    }
}