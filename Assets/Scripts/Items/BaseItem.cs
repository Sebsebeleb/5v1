using System.Collections.Generic;

using UnityEngine;

/// <summary>
/// Items that the player can equip or consume
/// </summary>
[System.Serializable]
public abstract class BaseItem
{
    protected string name;

    protected ItemUseType useType;

    protected ItemRarity rarity;

    protected List<ItemEffect> effects = new List<ItemEffect>();

    public enum ItemUseType
    {
        Equipment,
        Consumable
    }

    public enum ItemType
    {
        Weapon,
        Misc,
        Consumable
    }

    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary,
    }

    public string Name
    {
        get
        {
            return this.name;
        }
        set
        {
            this.name = value;
        }
    }

    /// <summary>
    /// Is the item consumable or equipment?
    /// </summary>
    public virtual ItemUseType UseType
    {
        get
        {
            return this.useType;
        }

        protected set
        {
            this.useType = value;
        }
    }

    public ItemRarity Rarity
    {
        get
        {
            return rarity;
        }
        set
        {
            rarity = value;
        }
    }

    // TODO: This is generation related, not really related to the item itself. Refactor?
    public void AddEffect(ItemEffect eff)
    {
        effects.Add(eff);
        eff.ItemGenerated(this);
    }

    public string GetDescription(bool richText)
    {
        string s = "";

        Debug.Log(effects.Count);

        foreach (ItemEffect eff in effects)
        {
            s += eff.GetDescription(this, richText);
        }

        return s;
    }
}