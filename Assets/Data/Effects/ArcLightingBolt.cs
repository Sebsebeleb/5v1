namespace BBG.Data.Effects
{
    using System.Collections.Generic;

    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects.ThemeDebuffs;
    using BBG.View;

    [System.Serializable]
    public class ArcLightingBolt : Effect
    {
        private int damage;

        public ArcLightingBolt(int damagePerJump)
        {
            this.IsInfinite = true;
            this.Description = new EffectDescription("Charged",
                (() => "This creature is charged by arc lighting. The lightning will strike a random adjacent target that does not have the Charged effect at end of turn, or, if no target is found, dissipate."));
            this.damage = damagePerJump;
        }

        protected override void Created()
        {
            EventManager.Register(Events.OnTurn, (OnTurn) this.DoJump);
        }

        protected override void Destroyed(){
            EventManager.UnRegister(Events.OnTurn, (OnTurn) this.DoJump);
        }

        private void DoJump()
        {
            List<Actor> validTargets = new List<Actor>();

            //Find a target
            foreach (Actor actr in GridManager.TileMap.GetAdjacent(this.owner.x, this.owner.y))
            {
                if (actr.gameObject.tag != "Corpse" && !actr.effects.HasEffect<Electrified>())
                {
                    validTargets.Add(actr);
                }
            }

            if (validTargets.Count > 0)
            {
                validTargets.Shuffle();

                Actor target = validTargets[0];
                target.damagable.TakeDamage(this.damage);
                this.ForceRemoveMe(true);
                target.effects.AddEffect(this);
                target.effects.AddEffect(new Electrified());

            }
            else{
                this.ForceRemoveMe(false);
            }
        }

        public override bool ShouldAnimate()
        {
            return true;
        }

        public override ChangeAnimation GetAnimationInfo()
        {
            ChangeAnimation a = new ChangeAnimation();
            a.SpawnHoverText = true;
            a.IconName = "Charged";

            return a;
        }

    }
}