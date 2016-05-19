namespace BBG.BaseClasses
{
    using System;
    using System.Runtime.Serialization;

    using BBG.Actor;
    using BBG.Interfaces;
    using BBG.View;

    [System.Serializable]
    public abstract class Effect : ITooltip, IAnimatableChange
    {

        protected EffectData data;


        // The ID that will actually be serialized to get a refernce
        protected int _ownerID;

        [NonSerialized]
        public Actor owner;

        private bool _ownerSet = false;

        // If it is infinite, it will not countdown and disappear on its own
        public bool IsInfinite = false;

        // The generic debuffs like Wet/Burning/Electrified etc.
        public bool IsThemeDebuff = false;
        // Is this effect a positive effect?
        public bool IsBuff = false;
        // Is this effect a negative effect? Note: effects can be neither buffs or debuffs, like ArcLighting effect
        public bool IsDebuff = false;
        // If it is purgable, effect cleansing will remove it
        public bool Purgable = false;
        public int Duration;
        // If it is a trait, it is inherit and not temporary, and should be described on the creature description as a trait rather than effect
        public bool IsTrait = false;
        // If hidden, it will not show up in inspector
        public bool IsHidden = false;

        protected EffectDescription Description = new EffectDescription("OOPS Missing description", () => "Oops");

        public Effect()
        {
            this.IsInfinite = true;
            this.Duration = -1;
            this.Created();
        }

        public Effect(int duration)
        {
            this.IsInfinite = false;
            this.Duration = duration;
            this.Created();
        }


        // FIXME: This system is broken.
        ~Effect()
        {
            this.Destroyed();
        }

        // Should be called when the effects wants to remove itself
        // DontDestory: if this is true, the effect itself will not be destroyed. Only the reference to it will be removed from the effectholder
        protected void ForceRemoveMe(bool DontDestroy=false)
        {
            this.owner.effects.RemoveEffect(this, DontDestroy);
        }

        // Called by the system to destroy this
        public void Destroy(){
            this.Destroyed();
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
            this.owner = who;
            this.OnAdded();
        }

        public string GetTooltip(){
            return this.Description.GetDescription();
        }

        public string GetName(){
            return this.Description.Name;
        }

        // IAnimatableChange methods

        public virtual bool ShouldAnimate(){
            return false;
        }
        public virtual ChangeAnimation GetAnimationInfo(){
            return new ChangeAnimation();
        }

        // Serialization

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context){
            info.AddValue("IsInfinite", this.IsInfinite);
            info.AddValue("Purgable", this.Purgable);
            info.AddValue("Duration", this.Duration);
            info.AddValue("IsTrait", this.IsTrait);
            info.AddValue("Description", this.Description);
            // We cannot serialize monobehaviours, so instead we save an ID that we can use
            info.AddValue("_actor", Utils.ActorToID(this.owner));
        }

                // This constructor is called when it is loaded from a saved game
        public Effect(SerializationInfo info, StreamingContext context){
            this.IsInfinite = info.GetBoolean("IsInfinite");
            this.Purgable = info.GetBoolean("Purgable");
            this.Duration = info.GetInt32("Duration");
            this.IsTrait = info.GetBoolean("IsTrait");
            // Set the reference based on id
            this.owner = Utils.IDToActor(info.GetInt32("_actor"));
        }

        public void _SetRawData(EffectData _data){
            this.data = _data;
        }

        public EffectData _GetRawData(){
            return this.data;
        }
    }

    [System.Serializable]
    // Holds the data for tooltip
    public class EffectDescription{
        public string Name;
        public Func<string> GetDescription;

        public EffectDescription(string name, Func<string> description){
            this.Name = name;
            this.GetDescription = description;
        }
    }

    [System.Serializable]
    public class EffectData{

    }
}
