using BaseClasses;
using Event;

namespace Data.Effects
{
    public class GrimParty : Effect
    {
        private const int BonusPerEnemy = 2;
        private int _currentBonus = 0;

        public GrimParty() : base(){
            IsTrait = true;
            
            Description = new EffectDescription(
                "Dead Party",
                describe
            );
        }

        private string describe(){
            return string.Format("Has {0} bonus attack for each alive enemy. \nCurrently: {1}",
                TextUtilities.Bold(TextUtilities.FontColor("#FF1111", BonusPerEnemy.ToString())),
                TextUtilities.Bold(TextUtilities.FontColor("#FF1111", _currentBonus.ToString()))
            );
        }

        protected override void Created()
        {
            base.Created();


            ActorDied callback = UpdateBonus;
            EventManager.Register(Events.ActorDied, callback);
            EventManager.Register(Events.OnTurn, (OnTurn) (() => {UpdateBonus();}));
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
            int numAliveEnemies = 0;

            foreach (Actor actor in GridManager.TileMap.GetAll())
            {
                if (actor.tag != "Corpse")
                {
                    numAliveEnemies++;
                }
            }

            int newBonus = numAliveEnemies * BonusPerEnemy;
            owner.attack.BonusAttack += (newBonus - _currentBonus);

            _currentBonus = newBonus;
        }

    }
}
