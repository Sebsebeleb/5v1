using BaseClasses;
using Event;

namespace Data.Effects
{


    /// <summary>
    /// Buffs all adjacent enemies when killed
    /// </summary>
    public class BuffAllyOnDeath : Effect
    {
        // ----------------------------
        // Only initalization stuff here
        // ----------------------------

        protected override void Created()
        {
            base.Created();

            ActorDied callback = OnActorDied;
            EventManager.Register(Events.ActorDied, callback);
        }

        protected override void Destroyed()
        {
            base.Destroyed();

            ActorDied callback = OnActorDied;
            EventManager.UnRegister(Events.ActorDied, callback);
        }

        // ----------------------------
        // Actual effect behaviour here
        // ----------------------------

        void OnActorDied(Actor who)
        {
            if (who == owner)
            {
                TriggerEffect();
                ForceRemoveMe();
            }
        }

        private void TriggerEffect()
        {
            foreach (Actor actor in GridManager.TileMap.GetAdjacent(owner.x, owner.y))
            {
                actor.effects.AddEffect(new BuffEffect());
            }
            int b = 2;
        }

        private class BuffEffect : Effect
        {
            public override void OnAdded()
            {
                base.OnAdded();

                owner.attack.BonusAttack += 2;
            }

            public override void OnRemoved()
            {
                base.OnRemoved();

                owner.attack.BonusAttack -= 2;
            }
        }

    }
}