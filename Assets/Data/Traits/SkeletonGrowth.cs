using BaseClasses;
using Event;
using System.Runtime.Serialization;
using System;
using UnityEngine;

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

            IsTrait = true;
        }

        private string describe(){
            return string.Format("Recieves {0} attack each time it acts.",
                TextUtilities.Bold(TextUtilities.FontColor("#FF1111", BonusPerTurn.ToString()))
            );
        }

        protected override void Created()
        {
            base.Created();


            OnActorActed callback = IncreaseStrength;
            EventManager.Register(Events.ActorActed, callback);
        }

        protected override void Destroyed()
        {
            base.Destroyed();

        }

        private void IncreaseStrength(Actor who)
        {
			if (who != owner){
                return;
            }

            owner.effects.AddEffect(new Boosted(1));

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
