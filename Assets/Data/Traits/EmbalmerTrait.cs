using BaseClasses;
using Event;

namespace Data.Effects
{
    public class EmbalmerTrait : Effect
    {
        private const int BonusPerCorpse = 2;
        private int _currentBonus = 0;


        protected override void Created()
        {
            base.Created();


            ActorDied callback = UpdateBonus;
            EventManager.Register(Events.ActorDied, callback);
            EventManager.Register(Events.OnTurn, callback);
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
