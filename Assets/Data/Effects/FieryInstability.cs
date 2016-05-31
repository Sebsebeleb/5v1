namespace BBG.Data.Effects
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects.ThemeDebuffs;
    using BBG.View;

    [System.Serializable]
    class FieryInstability : Effect
    {
        private readonly int _damage;

        public FieryInstability(int duration, int explodeDamage) : base(duration)
        {

            this.Description = new EffectDescription("Fiery Instability",
                this.describe
             );

            this._damage = explodeDamage;

        }

        private string describe()
        {
            string damageProp = TextUtilities.FontColor(Colors.DamageValue, this._damage);

            return string.Format("When this creature dies, it will deal {0} damage to all adjacent enemies and apply burning for 3 turns to them", damageProp);
        }

        protected override void Created()
        {
            base.Created();

            var callback = (PreActorDied)this.Explode;

            EventManager.Register(Events.ActorPreDied, callback);
        }

        protected override void Destroyed()
        {
            base.Destroyed();

            var callback = (PreActorDied)this.Explode;

            EventManager.UnRegister(Events.ActorPreDied, callback);
        }

        private void Explode(Actor who)
        {
            foreach (Actor enemy in GridManager.TileMap.GetAdjacent(this.owner.x, this.owner.y))
            {
                enemy.damagable.TakeDamage(this._damage);
                enemy.effects.AddEffect(new Burning(3));
            }
        }


        public override bool ShouldAnimate()
        {
            return true;
        }

        public override ChangeAnimation GetAnimationInfo()
        {
            ChangeAnimation a = new ChangeAnimation();
            a.SpawnHoverText = true;
            a.IconName = "Fiery Instability";

            return a;
        }
    }
}
