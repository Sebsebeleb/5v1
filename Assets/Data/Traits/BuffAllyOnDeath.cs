namespace BBG.Data.Traits
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects;

    /// <summary>
    /// Buffs all adjacent enemies when killed
    /// </summary>
    [System.Serializable]
    public class BuffAllyOnDeath : Effect
    {
        // ----------------------------
        // Only initalization stuff here
        // ----------------------------

        public BuffAllyOnDeath() : base(){
            this.IsTrait = true;

            this.Description = new EffectDescription(
                "Unstable Energies",
                this.describe
            );
        }

        private string describe(){
            return "When this creature dies, it will buff all adjacent enemies' attack by " +
            TextUtilities.Bold(TextUtilities.FontColor("#FF2222", "2"));
        }

        protected override void Created()
        {
            base.Created();

            ActorDied callback = this.OnActorDied;
            EventManager.Register(Events.ActorDied, callback);
        }

        protected override void Destroyed()
        {
            base.Destroyed();

            ActorDied callback = this.OnActorDied;
            EventManager.UnRegister(Events.ActorDied, callback);
        }

        // ----------------------------
        // Actual effect behaviour here
        // ----------------------------

        void OnActorDied(Actor who)
        {
            if (who == this.owner)
            {
                this.TriggerEffect();
                this.ForceRemoveMe();
            }
        }

        private void TriggerEffect()
        {
            foreach (Actor actor in GridManager.TileMap.GetAdjacent(this.owner.x, this.owner.y))
            {
                actor.effects.AddEffect(new AttackBuff(2));
            }
        }
    }
}