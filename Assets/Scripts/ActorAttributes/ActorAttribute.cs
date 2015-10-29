using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

// TODO: If neeeded, allow adding dynamic bonuses (that are calculated as this value is returned).
// Could be implemented as a list of methods to be called that return the modifier, which is called in the calculation, which is called in the calculation, which is called in the calculation, which is called in the calculation

/// <summary>
/// A generic attribute for an actor that can be modified in multiple ways. Used for stuff like stats.
/// </summary>
[System.Serializable]
public class ActorAttribute
{
    private readonly int startValue;

    private readonly string name;

    /// <summary>
    /// Initializes a new instance of the <see cref="ActorAttribute"/> class. 
    /// A generic attribute for an actor that can be modified in multiple ways. Used for stuff like stats.
    /// </summary>
    /// <param name="name">
    /// The display name of the stat
    /// </param>
    /// <param name="startValue">
    /// The initial base value for the attribute
    /// </param>
    public ActorAttribute(string name, int startValue)
    {
        this.startValue = startValue;
        this.name = name;
    }


    public int Value
    {
        get
        {
            return this.CalculateFinalValue();
        }
    }

    public string Name
    {
        get
        {
            return this.name;
        }
    }

    // Base values. Mostly by semi-permanent effects like items, zone modifiers etc.
    public int BaseValueBonus { get; set; }

    public float BaseValueMultiplier { get; set; }

    // Extra values (bonuses) Applied after base values. Should be used for temporary changes
    public int ExtraValueBonus { get; set; }

    public float ExtraValueMultiplier { get; set; }

    private int CalculateFinalValue()
    {
        int finalValue = this.startValue;

        // Apply base value modifications
        finalValue += this.BaseValueBonus;
        finalValue *= (int)(1 + this.BaseValueMultiplier);

        // Apply bonus value modifications
        finalValue += this.ExtraValueBonus;
        finalValue *= (int)(1 + this.ExtraValueMultiplier);

        return finalValue;
    }
}