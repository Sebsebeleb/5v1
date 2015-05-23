using BaseClasses;
using Event;
using UnityEngine;

namespace Data.Effects
{


    /// <summary>
    /// Buffs all adjacent enemies when killed
    /// </summary>
    public class BuffAllyOnDeath : Effect
    {
        protected override void Created()
        {
            Debug.Log("Helloo");
            base.Created();

            ActorDied callback = OnActorDied;
            EventManager.Register(Events.ActorDied, callback);
        }

        protected override void Destroyed()
        {
            Debug.Log("Helloo");
            base.Destroyed();

            ActorDied callback = OnActorDied;
            EventManager.UnRegister(Events.ActorDied, callback);
        }

        void OnActorDied(Actor who)
        {
            Debug.Log("Helloo");
            if (who == owner)
            {
                TriggerEffect();
                ForceRemove();
            }
        }

        private void TriggerEffect()
        {
            Debug.Log("Helloo");
            foreach (Actor actor in GridManager.TileMap.GetAdjacent(owner.x, owner.y))
            {
                actor.effects.AddEffect(new BuffEffect());
            }
            int b = 2;
        }

        private class BuffEffect : Effect
        {

        }

    }
}