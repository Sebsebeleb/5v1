using System;
using System.Collections.Generic;
using UnityEngine;

using BaseClasses;

namespace Data.Effects
{
    [System.Serializable]
    public class ArcLightingBolt : Effect
    {
        private int damage;

        public ArcLightingBolt(int damagePerJump)
        {
            IsInfinite = true;
            Description = new EffectDescription("Charged",
				(() => "This creature is charged by arc lighting. The lightning will strike a random adjacent target that does not have the Charged effect at end of turn, or, if no target is found, dissipate."));
            damage = damagePerJump;
        }

        protected override void Created()
        {
            Event.EventManager.Register(Event.Events.OnTurn, (Event.OnTurn) DoJump);
        }

        protected override void Destroyed(){
            Event.EventManager.UnRegister(Event.Events.OnTurn, (Event.OnTurn) DoJump);
        }

        private void DoJump(Actor who = null)
        {
            List<Actor> validTargets = new List<Actor>();

            //Find a target
            foreach (Actor actr in GridManager.TileMap.GetAdjacent(owner.x, owner.y))
            {
                if (actr.gameObject.tag != "Corpse" && !actr.effects.HasEffect(typeof(Electrified)))
                {
                    validTargets.Add(actr);
                }
            }

            if (validTargets.Count > 0)
            {
                validTargets.Shuffle();

                Actor target = validTargets[0];
                target.damagable.TakeDamage(damage);
                ForceRemoveMe(true);
                target.effects.AddEffect(this);
                target.effects.AddEffect(new Electrified());

            }
            else{
                ForceRemoveMe(false);
            }
        }
    }
}