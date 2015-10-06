using BaseClasses;
using Event;

namespace Data.Effects
{
    [System.Serializable]
    public class EmbalmerTrait : Effect
    {
        private const int BonusPerCorpse = 2;
        private int _currentBonus = 0;

        public EmbalmerTrait() : base(){
            Description = new EffectDescription(
                "Grave strength",
                describe
            );
        }

        private string describe(){
            return string.Format("Has {0} bonus attack for each adjacent corpse. \nCurrently: {1}",
                TextUtilities.Bold(TextUtilities.FontColor("#FF1111", BonusPerCorpse.ToString())),
                TextUtilities.Bold(TextUtilities.FontColor("#FF1111", _currentBonus.ToString()))
            );
        }

        protected override void Created()
        {
            base.Created();


            ActorDied callback = UpdateBonus;
            EventManager.Register(Events.ActorDied, callback);
            EventManager.Register(Events.OnTurn, (OnTurn)(()=>{UpdateBonus();}));
        }

        public override void OnRemoved()
        {
            base.OnRemoved();
            owner.attack.BonusAttack -= _currentBonus;
        }

        public override void OnAdded()
        {
            base.OnAdded();

            UpdateBonus();
        }

        private void UpdateBonus(Actor who = null)
        {
            int numAdjacentCorpses = 0;

            foreach (Actor actor in GridManager.TileMap.GetAdjacent(owner.x, owner.y))
            {
                if (actor.tag == "Corpse")
                {
                    numAdjacentCorpses++;
                }
            }

            int newBonus = numAdjacentCorpses * BonusPerCorpse;
            owner.attack.BonusAttack += (newBonus - _currentBonus);

            _currentBonus = newBonus;
        }

    }
}
