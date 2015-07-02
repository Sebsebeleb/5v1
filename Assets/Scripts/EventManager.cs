using System;
using System.Collections.Generic;

namespace Event
{

    public enum Events
    {
        OnTurn,
        OnCountedDown,
        OnActorTookDamage,
        ActorDied,
        PlayerLeveledUp,
        ActorActed, // Called when an enemy acts (it performs attacks etc.)
        PreEnemyAction, // Called right before an enemy executes an action, passign the action taken
        PreEnemmyEffectApplied, //Called right before an effect is applied to an enemy
    }

    #region delegates

    public delegate void OnTurn();

    public delegate void OnActorTookDamage(TookDamageArgs args);

    public delegate void ActorDied(Actor who);
    
    public delegate void PlayerLeveledUp(int level);
    
    public delegate void OnActorActed(Actor who);
    
    public delegate void OnPreEnemyAction(OnPreEnemyActionArgs args);
    
    public delegate void OnPreEnemyEffectApplied(PreEnemyEffectAppliedArgs args);

    #endregion


    #region argument structs

    public struct TookDamageArgs
    {
        public Actor Actor;
        public int Damage;

        public TookDamageArgs(Actor _actor, int _damage)
        {
            Actor = _actor;
            Damage = _damage;
        }
    }
    
    public struct OnPreEnemyActionArgs{
        public Actor who;
        public AI.AiAction action;
        
        public OnPreEnemyActionArgs(Actor _who, AI.AiAction _action){
            who = _who;
            action = _action;
        }
    }
    
    public struct PreEnemyEffectAppliedArgs{
        public Actor who;
        public BaseClasses.Effect effect;
        
        public PreEnemyEffectAppliedArgs(Actor _who, BaseClasses.Effect _effect){
            who = _who;
            effect = _effect;
        }
    }
    

    #endregion


    public static class EventManager
    {

        private static Dictionary<Events, List<Delegate>> _listeners = new Dictionary<Events, List<Delegate>>();

        //    private Dictionary<Delegate> 
        public static void Notify(Events ev, object args)
        {
            // Ignore event if not yet initalized
            if (!_listeners.ContainsKey(ev))
            {
                return;
            }
            foreach (Delegate listener in _listeners[ev])
            {
                listener.DynamicInvoke(args);
            }
        }

        public static void Register(Events evn, Delegate func)
        {
            // Create event if it doesnt exist
            if (!_listeners.ContainsKey(evn))
            {
                _listeners[evn] = new List<Delegate>();
            }

            _listeners[evn].Add(func);
        }

        public static void UnRegister(Events evn, Delegate func)
        {
            _listeners[evn].Remove(func);
        }
    }
}
