namespace BBG.Data.Effects.Specializations
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects.ThemeDebuffs;

    public class Pyromancer : Effect
    {
        private int currentBonus;

        public Pyromancer()
            : base()
        {
            this.IsTrait = true;
            this.Description = new EffectDescription("Pyromancer", this.Describe);
        }

        protected override void Created()
        {
            base.Created();

            ActorDied callback = this.UpdateBonus;
            EventManager.Register(Events.ActorDied, callback);
            EventManager.Register(Events.OnTurn, (OnTurn)(() => { this.UpdateBonus(); }));
        }

        private void UpdateBonus(Actor who = null)
        {
            int newBonus = 0;
            foreach (Actor actor in GridManager.TileMap.GetAll())
            {
                EffectHolder effs = actor.effects;

                if (effs.HasEffect<Burning>())
                {
                    newBonus += 1;
                }
            }

            this.owner.ManaRegen.ExtraValueBonus += newBonus - this.currentBonus;
            this.currentBonus = newBonus;
        }

        private string Describe()
        {
            return string.Format(@"Gain focus on<color=""red""> Fire</color> skills.
Gain +1 mana regen for each burning enemy (currently {0})", this.currentBonus);
        }
    }
}