using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BaseClasses;
using Data.Effects;
using Event;

namespace Data.Effects
{
    class FieryInstability : Effect
    {
        private readonly int _damage;

        public FieryInstability(int duration, int explodeDamage) : base(duration)
        {

            Description = new EffectDescription("Fiery Instability",
                describe
             );

            _damage = explodeDamage;

        }

        private string describe()
        {
            string damageProp = TextUtilities.FontColor(Colors.DamageValue, _damage);

            return string.Format("When this creature dies, it will deal {0} damage to all adjacent enemies and apply burning for 3 turns to them", damageProp);
        }

        protected override void Created()
        {
            base.Created();

            var callback = (PreActorDied)Explode;

            EventManager.Register(Events.ActorPreDied, callback);
        }

        protected override void Destroyed()
        {
            base.Destroyed();

            var callback = (PreActorDied)Explode;

            EventManager.UnRegister(Events.ActorPreDied, callback);
        }

        private void Explode(Actor who)
        {
            foreach (Actor enemy in GridManager.TileMap.GetAdjacent(owner.x, owner.y))
            {
                enemy.damagable.TakeDamage(_damage);
                enemy.effects.AddEffect(new Burning(3));
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
            a.IconName = "Fiery Instability";

            return a;
        }
    }
}
