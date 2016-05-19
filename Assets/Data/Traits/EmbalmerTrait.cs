namespace BBG.Data.Traits
{
    using BBG.Actor;
    using BBG.BaseClasses;

    [System.Serializable]
    public class EmbalmerTrait : Effect
    {
        private const int BonusPerCorpse = 2;
        private int _currentBonus = 0;

        public EmbalmerTrait() : base(){
            this.IsTrait = true;

            this.Description = new EffectDescription(
                "Grave strength",
                this.describe
            );
        }

        private string describe(){
            return string.Format("Has {0} bonus attack for each adjacent corpse. \nCurrently: {1}",
                TextUtilities.Bold(TextUtilities.FontColor("#FF1111", BonusPerCorpse.ToString())),
                TextUtilities.Bold(TextUtilities.FontColor("#FF1111", this._currentBonus.ToString()))
            );
        }

        protected override void Created()
        {
            base.Created();


            ActorDied callback = this.UpdateBonus;
            EventManager.Register(Events.ActorDied, callback);
            EventManager.Register(Events.OnTurn, (OnTurn)(()=>{this.UpdateBonus();}));
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
            int numAdjacentCorpses = 0;

            foreach (Actor actor in GridManager.TileMap.GetAdjacent(this.owner.x, this.owner.y))
            {
                if (actor.tag == "Corpse")
                {
                    numAdjacentCorpses++;
                }
            }

            int newBonus = numAdjacentCorpses * BonusPerCorpse;
            this.owner.attack.BonusAttack += (newBonus - this._currentBonus);

            this._currentBonus = newBonus;
        }

    }
}
