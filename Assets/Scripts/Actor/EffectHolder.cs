using BaseClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHolder : MonoBehaviour, IEnumerable
{
    //Reference
    private Actor actor;

    private List<Effect> _effects = new List<Effect>();

    void Awake()
    {
        actor = GetComponent<Actor>();
    }
    //Public api

    public void DoTurn()
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

    public void RemoveEffect(Effect effect)
    {
        effect.OnRemoved();
        _effects.Remove(effect);
    }

    public void AddEffect(Effect effect)
    {
        effect.SetOwner(actor);
        _effects.Add(effect);
    }

    public bool HasEffect(Effect effect)
    {
        return _effects.Contains(effect);
    }

    public List<Effect> GetEffectsOfType<T>()
    {
        return _effects.FindAll(effect => typeof(Effect) == typeof(T));
    }

    public IEnumerator GetEnumerator()
    {
        return _effects.GetEnumerator();
    }
}