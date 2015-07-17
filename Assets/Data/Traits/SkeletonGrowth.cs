using BaseClasses;
using Event;
using System.Runtime.Serialization;

namespace Data.Effects
{
    [System.Serializable]
    public class SkeletonGrowth : Effect
    {
        private const int BonusPerTurn = 1;
        private int _currentBonus = 0;

        public SkeletonGrowth() : base(){
            Description = new EffectDescription(
                "Gathering Energies",
                describe
            );
        }

        private string describe(){
            return string.Format("Recieves {1} each time it acts. \nCurrent boost: {0}",
                RichTextUtilities.Bold(RichTextUtilities.FontColor("#FF1111", _currentBonus.ToString())),
                RichTextUtilities.Bold(RichTextUtilities.FontColor("#FF1111", BonusPerTurn.ToString()))
            );
        }

        protected override void Created()
        {
            base.Created();


            OnActorActed callback = IncreaseStrength;
            EventManager.Register(Events.ActorActed, callback);
        }

        private void IncreaseStrength(Actor who)
        {
			if (who != owner){
                return;
            }

            _currentBonus++;
            owner.attack.BonusAttack++;

        }

        // Serialization and deserialization
        public override void GetObjectData(SerializationInfo info, StreamingContext context){
            base.GetObjectData(info, context);
            info.AddValue("_currentBonus", _currentBonus);
        }

        public SkeletonGrowth(SerializationInfo info, StreamingContext context) : base(info, context){
            _currentBonus = info.GetInt32("_currentBonus");
        }

    }
}
