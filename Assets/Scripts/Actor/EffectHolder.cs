using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EffectHolder : MonoBehaviour, IEnumerable
{
    //Reference
    private Actor actor;

    private List<Effect> _effects = new List<Effect>();
    // This list holds all traits, traits are related to the enemy type and will not be serialized/deserialized,
    // and thus should not be added after initalization of the actor
    private List<Effect> _traits = new List<Effect>();

    ~EffectHolder(){
        foreach(Effect eff in _effects){
            RemoveEffect(eff);
        }
    }

    void Awake()
    {
        actor = GetComponent<Actor>();
    }

    public void OnTurn() // broadcasted by countdown behaviour
    {
        DoTurn();
    }

    private void DoTurn()
    {
        List<Effect> deadEffects = new List<Effect>();

        foreach (Effect effect in _effects)
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
            RemoveEffect(deadEffect);
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
        _effects.Remove(effect);
    }

    // Performs deconstructor-ish actions like making sure all effects are properly destroyed
    public void Cleanup(){
        //.ToArray() to create a copy
        foreach(Effect eff in _effects.ToArray()){

            RemoveEffect(eff);
        }
    }

    public void AddEffect(Effect effect)
    {

        if (gameObject.tag != "Player"){
            Event.EventManager.Notify(Event.Events.PreEnemyEffectApplied, new Event.PreEnemyEffectAppliedArgs(actor, effect));
        }
        effect.SetOwner(actor);

        if (effect.IsTrait){
            _traits.Add(effect);
        }
        else{
            _effects.Add(effect);
        }
    }

    // Checks only if a similiar (same type) effect exists
    public bool HasEffect<T>(){
        foreach(Effect eff in _effects){
            if (eff is T){
                return true;
            }
        }

        return false;
    }

    //Checks this specific effect (wrong..?)
    public bool HasEffect(Effect effect)
    {
        return _effects.Contains(effect);
    }

    public List<Effect> GetEffectsOfType<T>()
    {
        return _effects.FindAll(effect => effect is T);
    }

    public IEnumerator GetEnumerator()
    {
        return _effects.GetEnumerator();
    }

    ///
    /// For save/load
    ///

    // Returns an array of all effects
    public Effect[] _GetRawData(){
        return _effects.ToArray();
    }

    public void _SetRawData(Effect[] _data){
        Effect[] deleteEffs = new Effect[_effects.Count];
        _effects.CopyTo(deleteEffs);
        foreach(Effect eff in deleteEffs){
            RemoveEffect(eff);
        }

        _effects.AddRange(_data);
    }
}