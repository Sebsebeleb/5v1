using UnityEngine;

[System.Serializable]
public class EquippableItem : BaseItem
{
    public EquippableItem()
    {
        this.Type = ItemType.Equipment;
        
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

    public void Equip()
    {
        Debug.Log("Hello, I was equipped" + this);
        foreach (var itemEffect in effects)
        {
            itemEffect.Equipped(this);
        }
    }

    public void UnEquip()
    {
        foreach (var itemEffect in effects)
        {
            itemEffect.UnEquipped(this);
        }
    }
}