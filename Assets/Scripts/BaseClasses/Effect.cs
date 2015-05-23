

namespace BaseClasses
{
    public class Effect
    {

        protected Actor owner;

        // If it is infinite, it will not countdown and disappear on its own
        public bool IsInfinite = false;

        // If it is purgable, effect cleansing will remove it
        public bool Purgable = false;
        public int Duration;

        public Effect(int duration = 0)
        {
            Duration = duration;
            Created();
        }

        ~Effect()
        {
            Destroyed();
        }

        // Called when this is effect is made, used for initalisation and subscribing to events
        protected virtual void Created()
        {

        }

        // Should do cleanup actions like unsubscribing from events. So: should ALWAYS be called when this object is cleaned up!!!
        protected virtual void Destroyed()
        {

        }

        // Should be called when this effect should be removed
        protected void ForceRemove()
        {
            owner.effects.RemoveEffect(this);
        }

        // Called when it is removed from owning actor
        public virtual void OnRemoved()
        {

        }

        // Called each turn
        public virtual void OnTurn()
        {

        }

        // Called when this is applied to an actor
        public void SetOwner(Actor who)
        {
            owner = who;
        }
    }
}
