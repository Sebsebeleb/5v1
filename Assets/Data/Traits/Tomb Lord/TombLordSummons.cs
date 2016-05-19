namespace BBG.Data.Traits
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using System;
    using System.Collections.Generic;

    // The tomb lord summons two random enemies from the current spawn list to his side.
    internal class TombLordSummons : Effect
    {

        public TombLordSummons()
        {
            this.IsTrait = true;
            this.IsHidden = true;
        }

        private const int numAllies = 2;
        public override void OnAdded()
        {
            base.OnAdded();

            AI.AiAction summonAction = new AI.AiAction();
            summonAction.Name = "Greater Reanimation";
            summonAction.AnimationName = "Attack";
            summonAction.Description = this.GetDescription;
            summonAction.Callback = this.DoSummon;
            summonAction.CalcPriority = () => -1;
            summonAction.IsFreeAction = true;

            this.owner.ai.AddAction(summonAction);
        }

        private string GetDescription()
        {
            return "Summons " + TextUtilities.Bold(TextUtilities.FontColor("#FF2222", numAllies.ToString()) + " random enemies from the current zone to his side");
        }

        private void DoSummon()
        {
            var enemies = GridManager.TileMap.GetAll();

            var PossibleSpots = new List<int[]>();

            foreach (var enemy in enemies)
            {
                if (enemy.tag == "Corpse")
                {
                    PossibleSpots.Add(new int[2] { enemy.x, enemy.y });
                }
            }

            PossibleSpots.Shuffle();

            int j = Math.Min(numAllies, PossibleSpots.Count);
            for (int i = 0; i < j; i++)
            {
                this._summonAlly(PossibleSpots[i]);
            }
        }

        private void _summonAlly(int[] position)
        {
            int x = position[0];
            int y = position[1];

            EnemyManager.SpawnRandomEnemy(x, y);
        }
    }
}