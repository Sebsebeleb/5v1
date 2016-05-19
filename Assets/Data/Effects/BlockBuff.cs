namespace BBG.Data.Effects
{
    using BBG.BaseClasses;

    [System.Serializable]
    internal class Blocking : Effect
    {
        public Blocking(int duration)
            : base(duration)
        {
            this.IsBuff = true;
            this.Description = new EffectDescription(
                "Blocking",
                this.describe
                );
        }

        private string describe(){
            return "You are invulnerable!";
        }

        public override void OnAdded()
        {
            base.OnAdded();

            this.owner.damagable.IsInvulnerable += 1;
        }

        public override void OnRemoved()
        {
            base.OnRemoved();

            this.owner.damagable.IsInvulnerable -= 1;
        }
    }
}