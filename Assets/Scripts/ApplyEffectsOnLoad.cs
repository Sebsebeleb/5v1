//Instatiates and adds to EventHolder all specified (by name) of this creature

using BaseClasses;
using UnityEngine;

public class ApplyEffectsOnLoad : MonoBehaviour
{
    public string[] InitialEffects;
    // These are added even if the game is loaded
    public string[] Traits;

    private EffectHolder _effects;

    private void Awake()
    {
        _effects = GetComponent<EffectHolder>();
    }

    private void Start()
    {
        AddTraits();
    }

    public void OnSpawn(){
        AddEffects();
    }

    // Call this to apply the effects.
    private void AddEffects(){
        foreach (string effectName in InitialEffects)
        {
            Effect eff = GameResources.LoadEffect(effectName);
            _effects.AddEffect(eff);
        }
    }

    private void AddTraits(){
        foreach(string effectName in Traits){
            Effect eff = GameResources.LoadEffect(effectName);
            _effects.AddEffect(eff);
        }
    }

}