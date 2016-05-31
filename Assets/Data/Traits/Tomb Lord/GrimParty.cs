namespace BBG.Data.Traits
{
    using BBG.Actor;
    using BBG.BaseClasses;

    [System.Serializable]
    public class GrimParty : Effect
    {
        private const int BonusPerEnemy = 2;
        private int _currentBonus = 0;

        public GrimParty() : base()
        {
            this.IsTrait = true;

            this.Description = new EffectDescription(
                "Dead Party",
                this.describe
            );
        }

        private string describe()
        {
            return string.Format("Has {0} bonus attack for each alive enemy. \nCurrently: {1}",
                TextUtilities.Bold(TextUtilities.FontColor("#FF1111", BonusPerEnemy.ToString())),
                TextUtilities.Bold(TextUtilities.FontColor("#FF1111", this._currentBonus.ToString()))
            );
        }

        protected override void Created()
        {
            base.Created();


            ActorDied callback = this.UpdateBonus;
            EventManager.Register(Events.ActorDied, callback);
            EventManager.Register(Events.OnTurn, (OnTurn)(() => { this.UpdateBonus(); }));
        }

        public override void OnRemoved()
        {
            base.OnRemoved();
            this.owner.attack.BonusAttack -= this._currentBonus;
        }

        public override void OnAdded()
        {
            base.OnAdded();

            this.UpdateBonus();
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
            this.owner.attack.BonusAttack += (newBonus - this._currentBonus);

            this._currentBonus = newBonus;
        }

    }
}
