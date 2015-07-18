﻿//Instatiates and adds to EventHolder all specified (by name) of this creature

using BaseClasses;
using System;
using System.Globalization;
using System.Reflection;
using UnityEngine;

public class ApplyEffectsOnLoad : MonoBehaviour
{
    public string[] InitialEffects;
    // These are added even if the game is loaded
    public string[] Traits;

    private static Assembly assembly = Assembly.GetCallingAssembly();
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
            Effect eff = LoadEffect(effectName);
            _effects.AddEffect(eff);
        }
    }

    private void AddTraits(){
        foreach(string effectName in Traits){
            Effect eff = LoadEffect(effectName);
            _effects.AddEffect(eff);
        }
    }

    /// <summary>
    /// Finds an effect by name and returns a new instance
    /// </summary>
    /// <param name="name">Name of effect to find</param>
    /// <returns></returns>
    private static Effect LoadEffect(string name)
    {

        var typ = assembly.GetType("Data.Effects." + name);
        var effInstance = Activator.CreateInstance(typ);
        var test = effInstance as Effect;

        return test;

    }
}