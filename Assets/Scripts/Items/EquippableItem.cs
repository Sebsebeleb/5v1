using UnityEngine;

[System.Serializable]
public class EquippableItem : BaseItem
{
    public EquippableItem()
    {
        this.UseType = ItemUseType.Equipment;
        
    }

    private EquipmentType typeOfEquipment;

    public enum EquipmentType
    {
        Weapon,
        Misc,
    }

    public EquipmentType TypeOfEquipment
    {
        get
        {
            return this.typeOfEquipment;
        }
        set
        {
            this.typeOfEquipment = value;
        }
    }

    public void Equip(Actor wearer)
    {
        Debug.Log("Hello, I was equipped" + this);
        foreach (var itemEffect in effects)
        {
            itemEffect.Equipped(this, wearer);
        }
    }

    public void UnEquip(Actor wearer)
    {
        foreach (var itemEffect in effects)
        {
            itemEffect.UnEquipped(this, wearer);
        }
    }
}