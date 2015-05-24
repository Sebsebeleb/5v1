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

            owner.ai.AddAction(() => new AI.ActionPriority(-1, DoBuff), freeAction:true);
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