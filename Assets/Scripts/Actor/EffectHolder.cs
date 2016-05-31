namespace BBG.Actor
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using BBG.BaseClasses;

    using UnityEngine;
    using UnityEngine.Assertions;

    public class EffectHolder : MonoBehaviour, IEnumerable
    {
        //Reference
        private Actor actor;

        private List<Effect> _effects = new List<Effect>();
        // This list holds all traits, traits are related to the enemy type and will not be serialized/deserialized,
        // and thus should not be added after initalization of the actor
        private List<Effect> _traits = new List<Effect>();

        ~EffectHolder(){
            foreach(Effect eff in this._effects){
                this.RemoveEffect(eff);
            }
        }

        void Awake()
        {
            this.actor = this.GetComponent<Actor>();
        }

        public void OnTurn() // broadcasted by countdown behaviour
        {
            this.DoTurn();
        }

        private void DoTurn()
        {
            List<Effect> deadEffects = new List<Effect>();

            foreach (Effect effect in this._effects.ToArray())
            {
                effect.OnTurn();

                if (!effect.IsInfinite)
                {
                    effect.Duration--;
                    if (effect.Duration <= 0)
                    {
                        deadEffects.Add(effect);
                    }
                }
            }

            foreach (Effect deadEffect in deadEffects) {
                this.RemoveEffect(deadEffect);
            }
        }

        //Public api

        ///
        /// DontDestroy: Only remove the reference, do not actually destroy the effect itself
        ///
        public void RemoveEffect(Effect effect, bool DontDestroy=false)
        {
            if (!DontDestroy){
                effect.OnRemoved();
                effect.Destroy();
            }
            this._effects.Remove(effect);
        }

        // Performs deconstructor-ish actions like making sure all effects are properly destroyed
        public void Cleanup(){
            //.ToArray() to create a copy
            foreach(Effect eff in this._effects.ToArray()){

                this.RemoveEffect(eff);
            }
        }

        public void AddEffect(Effect effect)
        {
            // Check that effects arre properly hidden
            Assert.IsFalse(effect.GetName() == "OOPS Missing description" && !effect.IsHidden, "WARNING: Effect should be hidden or given a proper description: " + effect.GetType().ToString());

            if (this.gameObject.tag != "Player"){
                EventManager.Notify(Events.PreEnemyEffectApplied, new PreEnemyEffectAppliedArgs(this.actor, effect));
            }
            effect.SetOwner(this.actor);

            /*if (effect.IsTrait){
                this._traits.Add(effect);
            }
            else{
                this._effects.Add(effect);
            }*/

            this._effects.Add(effect);
        }

        // Checks only if a similiar (same type) effect exists
        public bool HasEffect<T>(){
            foreach(Effect eff in this._effects){
                if (eff is T){
                    return true;
                }
            }

            return false;
        }

        //Checks this specific effect (wrong..?)
        public bool HasEffect(Effect effect)
        {
            return this._effects.Contains(effect);
        }

        public List<Effect> GetEffectsOfType<T>()
        {
            return this._effects.FindAll(effect => effect is T);
        }

        public Effect[] GetTraits(){
            return this._traits.ToArray();
        }

        public Effect[] GetEffects(){
            return this._effects.ToArray();
        }

        public IEnumerator GetEnumerator()
        {
            return this._effects.GetEnumerator();
        }

        ///
        /// For save/load
        ///

        // Returns an array of all effects
        public Effect[] _GetRawData(){
            return this._effects.ToArray();
        }

        public void _SetRawData(Effect[] _data){
            Effect[] deleteEffs = new Effect[this._effects.Count];
            this._effects.CopyTo(deleteEffs);
            foreach(Effect eff in deleteEffs){
                this.RemoveEffect(eff);
            }

            this._effects.AddRange(_data);
        }

        public Effect[] GetRawDataYo()
        {
            return this._effects.ToArray();
        }

        public void SetRawDataYo(Effect[] effects)
        {
            this._effects = effects.ToList();
        }
    }
}