namespace BBG
{
    using System;
    using System.Collections.Generic;

    using BBG.Actor;

    /* Event descriptions

        Identifier ( Arguments )
        OnTurn ( )
            Called when a turn passes


        PlayerAttackCommand ( Enemy actor )
            Actor - What enemy was attacked
            Called when the player issues an attack command (standard click), meaning it won't trigger on skills that use standard attacks
            Triggers after the attack

    */

    public enum Events
    {
        OnTurn, // Called whenever a turn passes
        ActorCountedDown, // Called whenever an actor has it's countdown decreased by a turn manager (so not while stunned)
        OnActorTookDamage,
        ActorPreDied, // Called just before an enemy is killed, right before effects are cleaned up
        ActorDied, // Called after an enemy is killed and effects have been cleaned up
        PlayerLeveledUp,
        ActorActed, // Called when an enemy acts (it performs attacks etc.)
        PreEnemyAction, // Called right before an enemy executes an action, passign the action taken
        PreEnemyEffectApplied, //Called right before an effect is applied to an enemy

        // Some events that aren't related to actual gameplay
        GameDeserialized, // Called after the game has loaded a save, used by EnemyDisplay so it knows to update itself
        PostEnemySpawned, // Called JUST after an enemy is spawned.
        EnemyAttack, // Called whenever an enemy attacks the player with a standard attack. Passes the damage dealt and who attacked
        PlayerAttackCommand, // Called when the player issues an attack command (so does not trigger on skills that use regular attack)
    }

    #region delegates

    public delegate void ActorParameters(Actor.Actor who);

    public delegate void OnTurn();

    public delegate void OnActorTookDamage(TookDamageArgs args);
    public delegate void PreActorDied(Actor.Actor who);

    public delegate void ActorDied(Actor.Actor who);

    public delegate void PlayerLeveledUp(int level);

    public delegate void OnActorActed(Actor.Actor who);

    public delegate void OnPreEnemyAction(OnPreEnemyActionArgs args);

    public delegate void OnPreEnemyEffectApplied(PreEnemyEffectAppliedArgs args);

    public delegate void OnPostEnemySpawned(Actor.Actor who);

    public delegate void OnEnemyAttack(EnemyAttackArgs args);

    // Events that are not related to actual gameplay

    public delegate void OnGameDeserialized();

    // Who we attacked
    public delegate void OnPlayerAttackDommand(Actor.Actor who);


    #endregion


    #region argument structs

    public struct TookDamageArgs
    {
        public Actor.Actor Actor;
        public int Damage;

        public TookDamageArgs(Actor.Actor _actor, int _damage)
        {
            this.Actor = _actor;
            this.Damage = _damage;
        }
    }

    public struct OnPreEnemyActionArgs
    {
        public Actor.Actor who;
        public AI.AiAction action;

        public OnPreEnemyActionArgs(Actor.Actor _who, AI.AiAction _action)
        {
            this.who = _who;
            this.action = _action;
        }
    }

    public struct PreEnemyEffectAppliedArgs
    {
        public Actor.Actor who;
        public BaseClasses.Effect effect;

        public PreEnemyEffectAppliedArgs(Actor.Actor _who, BaseClasses.Effect _effect)
        {
            this.who = _who;
            this.effect = _effect;
        }
    }

    public struct EnemyAttackArgs
    {
        public Actor.Actor who;
        public int rawDamage;
    }


    #endregion


    public static class EventManager
    {

        // Dead callbacks that are to be removed
        private static Dictionary<Events, List<Delegate>> _deadListeners = new Dictionary<Events, List<Delegate>>();

        private static Dictionary<Events, List<Delegate>> _listeners = new Dictionary<Events, List<Delegate>>();

        // Are we in the middle of processing an event?
        private static bool IsProcessing;

        //    private Dictionary<Delegate>
        public static void Notify(Events ev, object args)
        {

            // Ignore event if not yet initalized
            if (!_listeners.ContainsKey(ev))
            {
                return;
            }
            foreach (Delegate listener in _listeners[ev].ToArray())
            {
                if (args == null || listener.Method.GetParameters().Length == 0){
                    listener.DynamicInvoke();
                }
                else{
                    listener.DynamicInvoke(args);
                }
            }
        }

        /// Register a new callback to be called for a specific event
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
