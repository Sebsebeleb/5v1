using BaseClasses;
using Event;

namespace Data.Effects
{
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

    }
}
