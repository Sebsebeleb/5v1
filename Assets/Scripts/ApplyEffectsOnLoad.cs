﻿//Instatiates and adds to EventHolder all specified (by name) of this creature
using BaseClasses;
using System;
using System.Reflection;
using UnityEngine;

public class ApplyEffectsOnLoad : MonoBehaviour
{
    public string[] InitialEffects;

    private static Assembly assembly = Assembly.GetCallingAssembly();
    private EffectHolder _effects;

    private void Awake()
    {
        _effects = GetComponent<EffectHolder>();
    }

    private void Start()
    {
        assembly.GetName();
        foreach (string effectName in InitialEffects)
        {
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

        var effInstance = Activator.CreateInstance(assembly.FullName, "Data.Effects." + name);
        var test = effInstance.Unwrap() as Effect;

        return test;

    }
}