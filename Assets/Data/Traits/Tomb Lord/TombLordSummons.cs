using System;
using System.Collections.Generic;
using BaseClasses;
using UnityEngine;

namespace Data.Effects
{
    // The tomb lord summons two random enemies from the current spawn list to his side.
    internal class TombLordSummons : Effect
    {
        private const int numAllies = 2;
        public override void OnAdded()
        {
            base.OnAdded();

            AI.AiAction summonAction = new AI.AiAction();
            summonAction.Name = "Greater Reanimation";
            summonAction.AnimationName = "Attack";
            summonAction.Description = GetDescription;            
            summonAction.Callback = DoSummon;
            summonAction.CalcPriority = () => -1;
            summonAction.IsFreeAction = true;
            
            owner.ai.AddAction(summonAction);
        }
        
        private string GetDescription(){
            return "Summons " + RichTextUtilities.Bold(RichTextUtilities.FontColor("#FF2222", numAllies.ToString()) + " random enemies from the current zone to his side");
        }

        private void DoSummon()
        {
            var enemies = GridManager.TileMap.GetAll();

            var PossibleSpots = new List<int[]>();

            foreach (var enemy in enemies) {
                if (enemy.tag == "Corpse") {
                PossibleSpots.Add(new int[2]{enemy.x, enemy.y});
                }
            }

            PossibleSpots.Shuffle();
            
            int j = Math.Min(numAllies, PossibleSpots.Count);
            for(int i = 0; i<j; i++){
                _summonAlly(PossibleSpots[i]);
            }
        }
        
        private void _summonAlly(int[] position){
            int x = position[0];
            int y = position[1];
            
            EnemyManager.SpawnRandomEnemy(x, y);
        }
    }
}