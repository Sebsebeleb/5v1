using System;
using System.Collections.Generic;

namespace Event
{

    public enum Events
    {
        OnTurn,
        OnCountedDown,
        OnActorTookDamage,
    }

    #region delegates

    public delegate void OnTurn();

    public delegate void OnCountedDown(CountedDownArgs args);

    public delegate void OnActorTookDamage(TookDamageArgs args);

    #endregion


    #region argument structs

    public struct TookDamageArgs
    {
        public Enemy Actor;
        public int Damage;

        public TookDamageArgs(Enemy _actor, int _damage)
        {
            Actor = _actor;
            Damage = _damage;
        }
    }

    public struct CountedDownArgs
    {
        public Enemy Actor;

        public CountedDownArgs(Enemy actor)
        {
            Actor = actor;
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
            if (!_listeners.ContainsKey(ev)) {
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
            if (!_listeners.ContainsKey(evn)) {
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
