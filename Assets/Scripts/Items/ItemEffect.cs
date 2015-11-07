using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml;

using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Effects that can be put on equipment to implement behaviour. For example, implementing the "+1 damage to fire skills" part of an item.
/// </summary>
public abstract class ItemEffect
{

    private static Assembly asm = Assembly.GetCallingAssembly();

    public static ItemEffect LoadFromXML(XmlElement node, BaseItem.ItemRarity rarity)
    {

        List<object> parameters = new List<object>();

        Debug.Log("Looking for rarity: " + rarity.ToString());
        XmlNode para = node.SelectSingleNode(rarity.ToString().ToLower()).SelectSingleNode("parameters");

        foreach (XmlElement parameterValue in para)
        {
            parameters.Add(parameterValue.InnerText);
        }

        Debug.Log("Trying to load effect: " + node.FirstChild.Value);
        Debug.Log(node.FirstChild.Name);
        Debug.Log(node.FirstChild.InnerText);
        
        Debug.Log("with these parameters: " + parameters);

        ItemEffect eff = BindEffect(node.FirstChild.InnerText, parameters.ToArray());


        return eff;
    }

    /// <summary>
    /// Uses reflection to link a 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private static ItemEffect BindEffect(string pathName, object[] parameters)
    {
        Debug.Log("Trying to load: Assets.Data.ItemEffects." + pathName);
        var v = asm.GetType("Assets.Data.ItemEffects." + pathName);
        Debug.Log(v);

        Assert.IsNotNull(v, "Error trying to load ItemEffect: " + pathName + ", tried to load it from: " + "Assets.Data.ItemEffects." + pathName);

        // All item effects should only have a single constructor, so we assume that is true.
        Assert.IsTrue(v.GetConstructors().Length == 1);
        ConstructorInfo ctor = v.GetConstructors()[0];

        // Convert the strings from the xml into proper types
        object[] convertedParameters = ConvertParameters(ctor, parameters);

        // Now initalize the effect we found, using the converted parameters
        ItemEffect effect = ctor.Invoke(convertedParameters) as ItemEffect;
        
        return effect;
    }

    /// <summary>
    /// Tries to convert the string parameters from the xml into the type that is required by the constructor.
    /// TODO: Currently only converts int values. Do we actually need more? Quite possibly bool?
    /// </summary>
    /// <param name="ctor">The constructor info</param>
    /// <param name="parameters">The parameters that we want to try to convert</param>
    /// <returns>The converted parameters</returns>
    private static object[] ConvertParameters(ConstructorInfo ctor, object[] parameters)
    {
        Debug.Log("Trying to convert parameters");
        object[] converted = new object[parameters.Length];
        var needed = ctor.GetParameters();

        for (int i = 0; i < parameters.Length; i++)
        {
            Type neededType = needed[i].ParameterType;

            Debug.Log(neededType);
            if (neededType == typeof(int))
            {
                Debug.Log(parameters[i].ToString());
                int result;
                bool success = int.TryParse(parameters[i].ToString(), out result);
                Assert.IsTrue(
                    success,
                    "ItemEffect Parameter Conversion failed, the given parameter could not be convert to int");

                Debug.Log("Converted parameter to: " + result);

                converted[i] = result;
            }
            else
            {
                Debug.LogError("Parameter was not converted");
            }

        }

        return converted;
    }

    /// <summary>
    /// Called when the item is being created, and this effect is added to it
    /// </summary>
    /// <param name="item">The item this effect is part of</param>
    public virtual void ItemGenerated(BaseItem item)
    {
    }

    /// <summary>
    /// Called when an item is equipped to the player
    /// </summary>
    public virtual void Equipped(BaseItem item)
    {
    }

    /// <summary>
    /// Called when the item is removed from the player
    /// </summary>
    public virtual void UnEquipped(BaseItem item)
    {
    }

    /// <summary>
    /// Called when the item is being described by a tooltip or similiar. Should something that describes what this effect does
    /// </summary>
    /// <param name="richText">Should the string be rich text styled?</param>
    /// <returns>Description for this part of the description</returns>
    public abstract string GetDescription(BaseItem item, bool richText);

    /// <summary>
    /// For consumable items, this is called when the player wants to use it
    /// </summary>
    public virtual void OnUse(BaseItem item)
    {
    }

    /// <summary>
    /// For targetable consumable items, this is called when the player tries to use it on a map position
    /// </summary>
    public virtual void OnUseOnGrid(BaseItem item, int x, int y)
    {
    }

    /// <summary>
    /// For consumables: Can we consume it right now?
    /// For equipment: Can we equip this?
    /// </summary>
    public virtual void CanUse(BaseItem item)
    {
    }

    /// <summary>
    /// For consumables: Can we use this on target grid?
    /// </summary>
    public virtual void CanUseOnGrid(BaseItem item)
    {
    }
}
