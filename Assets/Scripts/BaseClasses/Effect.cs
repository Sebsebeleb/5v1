using System;

namespace BaseClasses
{
    public abstract class Effect : ITooltip, IAnimatableChange
    {

        protected Actor owner;

        // If it is infinite, it will not countdown and disappear on its own
        public bool IsInfinite = false;

        // If it is purgable, effect cleansing will remove it
        public bool Purgable = false;
        public int Duration;
        // If it is a trait, it is inherit and not temporary, and should be described on the creature description as a trait rather than effect
        public bool IsTrait = false;
        
        protected EffectDescription Description = new EffectDescription("OOPS Missing description", () => "Oops");

        public Effect()
        {
            IsInfinite = true;
            Duration = -1;
            Created();
        }
        public Effect(int duration)
        {
            Duration = duration;
            Created();
        }

        ~Effect()
        {
            Destroyed();
        }

        // Should be called when the effects wants to remove itself
        protected void ForceRemoveMe()
        {
            owner.effects.RemoveEffect(this);
        }

        
        // ------------------
        // Use these ONLY for initalization and deconstruction of stuff like event registering
        // TODO: Maybe these should be done in overrided constructor and deconstructor instead of having explicit function for them?
        // ------------------

        // Called when this is effect is made, used for initalisation and subscribing to events
        protected virtual void Created()
        {

        }

        // Should do cleanup actions like unsubscribing from events. So: should ALWAYS be called when this object is cleaned up!!!
        protected virtual void Destroyed()
        {

        }


        // ------------------
        // Use these functions for actually doing stuff to the actor
        // ------------------

        // called when it is actually added to an actor and Owner is initalized
        public virtual void OnAdded()
        {
            
        }

        // Called when it is removed from owning actor
        public virtual void OnRemoved()
        {

        }


        // Called each turn
        public virtual void OnTurn()
        {

        }

        // -------------------
        // These are called by the system
        // -------------------

        // Called when this is applied to an actor
        public void SetOwner(Actor who)
        {
            owner = who;
            OnAdded();
        }
        
        public string GetTooltip(){
            return Description.GetDescription();
        }
        
        public string GetName(){
            return Description.Name;
        }
        
        // IAnimatableChange methods
        
        public virtual bool ShouldAnimate(){
            return false;
        }
        public virtual ChangeAnimation GetAnimationInfo(){
            return new ChangeAnimation();
        }

    }
    
    // Holds the data for tooltip 
    public class EffectDescription{
        public string Name;
        public Func<string> GetDescription;
        
        public EffectDescription(string name, Func<string> description){
            Name = name;
            GetDescription = description;
        }
    }
    
}
