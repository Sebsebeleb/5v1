//Defines commonly used color coding for different text.

using UnityEngine;
using Color = UnityEngine.Color; 

public static class Colors{
    public const string DamageValue = "#FF4D4D";
    public const string DurationValue = "#4DB870";
    public const string HealValue = "#F0466A";

    // Specific words mostly used by the prettifier
    public const string AttackWord = "#f65a52"; // TODO: Too redundant? confusing because of the difference between the verb and the noun?
    public const string DamageWord = "#FFBE4D";
    public const string Stunned = "#3385FF";
    public const string EffectApplied = "#A261F2"; // "Applied"
    public const string You = "#9BF261";
    public const string Fire = "#F49132";
    // Effects
    public const string Burning = "#F49132";
    public const string Wet = "#237ec6";
    public const string Electrified = "#eae532";

    // Inventory colors
    public static Color InventoryWeaponColor = new Color(0, 0.47f, 0.69f, 1f);
    public static Color InventoryMiscColor = new Color(0.86f, 0.72f, 0.56f, 1f);
}