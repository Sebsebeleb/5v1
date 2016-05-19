namespace BBG.Data.Effects
{
    using BBG.BaseClasses;

    [System.Serializable]
    internal class Judged : Effect
    {
        public Judged(int duration)
            : base(duration)
        {
            this.Description = new EffectDescription(
                "Judged",
                this.describe
                );
        }

        private string describe(){
            return "You cannot use skills!";
        }

        public override void OnAdded()
        {
            base.OnAdded();

            this.owner.status.SetSilenced(true);
        }

        public override void OnRemoved()
        {
            base.OnRemoved();

            this.owner.status.SetSilenced(false);
        }
    }
}