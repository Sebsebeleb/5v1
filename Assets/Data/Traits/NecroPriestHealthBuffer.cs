namespace BBG.Data.Traits
{
    using System.Collections.Generic;

    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects;
    using BBG.View;

    // The priest will buff a random enemy's attack each turn
    internal class NecroPriestHealthBuffer : Effect
    {
        private int _attackBonus = 2;

        public NecroPriestHealthBuffer() : base() {
            this.IsHidden = true;
            this.IsTrait = true;
        }


        public override void OnAdded()
        {
            base.OnAdded();

            AI.AiAction buffAction = new AI.AiAction();
            buffAction.Name = "Priestly Buff";
            buffAction.AnimationName = "Attack";
            buffAction.Description = this.GetDescription;
            buffAction.Callback = this.DoBuff;
            buffAction.CalcPriority = () => -1;
            buffAction.IsFreeAction = true;
            buffAction.animateThis = true;

            //Animation info
            buffAction.AnimationInfo = new ChangeAnimation();
            buffAction.AnimationInfo.SpawnHoverText = true;

            this.owner.ai.AddAction(buffAction);
        }

        private string GetDescription(){
            return "Buffs a random creature, increasing it's attack by " + TextUtilities.Bold(TextUtilities.FontColor("#FF2222", "2"));
        }

        private void DoBuff()
        {
            var enemies = GridManager.TileMap.GetAll();

            var PossibleTargets = new List<Actor>();

            foreach (var enemy in enemies) {
                if (enemy.tag != "Corpse" && enemy != this.owner) {
                PossibleTargets.Add(enemy);
                }
            }

            if (PossibleTargets.Count > 0)
            {
                PossibleTargets.Shuffle();
                PossibleTargets[0].effects.AddEffect(new AttackBuff(this._attackBonus));
            }
        }
    }
}