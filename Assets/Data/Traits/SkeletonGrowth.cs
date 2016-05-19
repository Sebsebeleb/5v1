namespace BBG.Data.Traits
{
    using System.Runtime.Serialization;

    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects.Enemies;

    [System.Serializable]
    public class SkeletonGrowth : Effect
    {
        private const int BonusPerTurn = 1;
        private int _currentBonus = 0;

        public SkeletonGrowth() : base(){
            this.Description = new EffectDescription(
                "Gathering Energies",
                this.describe
            );

            this.IsTrait = true;
        }

        private string describe(){
            return string.Format("Recieves {0} attack each time it acts.",
                TextUtilities.Bold(TextUtilities.FontColor("#FF1111", BonusPerTurn.ToString()))
            );
        }

        protected override void Created()
        {
            base.Created();


            OnActorActed callback = this.IncreaseStrength;
            EventManager.Register(Events.ActorActed, callback);
        }

        protected override void Destroyed()
        {
            base.Destroyed();

        }

        private void IncreaseStrength(Actor who)
        {
			if (who != this.owner){
                return;
            }

            this.owner.effects.AddEffect(new Boosted(1));

        }

        // Serialization and deserialization
        public override void GetObjectData(SerializationInfo info, StreamingContext context){
            base.GetObjectData(info, context);
            info.AddValue("_currentBonus", this._currentBonus);
        }

        public SkeletonGrowth(SerializationInfo info, StreamingContext context) : base(info, context){
            this._currentBonus = info.GetInt32("_currentBonus");
        }

    }
}
