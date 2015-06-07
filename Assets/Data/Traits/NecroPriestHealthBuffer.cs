using System.Collections.Generic;
using BaseClasses;
using UnityEngine;

namespace Data.Effects
{
    // The priest will buff a random enemy's attack each turn
    internal class NecroPriestHealthBuffer : Effect
    {
        private int _attackBonus = 2;


        public override void OnAdded()
        {
            base.OnAdded();

            AI.AiAction buffAction = new AI.AiAction();
            buffAction.Name = "Priestly Buff";
            buffAction.Description = "Buffs a random creature, increasing it's attack by " + RichTextUtilities.FontColor("#FF2222", "2") + ".";            
            buffAction.Callback = DoBuff;
            buffAction.CalcPriority = () => -1;
            buffAction.IsFreeAction = true;
            
            owner.ai.AddAction(buffAction);
        }

        private void DoBuff()
        {
            var enemies = GridManager.TileMap.GetAll();

            var PossibleTargets = new List<Actor>();

            foreach (var enemy in enemies) {
                if (enemy.tag != "Corpse" && enemy != owner) {
                PossibleTargets.Add(enemy);
                }
            }

            if (PossibleTargets.Count > 0)
            {
                PossibleTargets.Shuffle();
                PossibleTargets[0].effects.AddEffect(new AttackBuff(_attackBonus));
            }
        }
    }
}