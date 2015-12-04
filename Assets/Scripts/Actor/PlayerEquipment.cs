using System.Collections;
using System.Collections.Generic;

using Generation;

using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// The player inventory.
/// </summary>
internal class PlayerEquipment : MonoBehaviour, IEnumerable<BaseItem>
{
    private List<BaseItem> backpack = new List<BaseItem>();

    private EquippableItem equippedWeapon;
    private BaseItem[] equippedMisc = new EquippableItem[6];

    /// <summary>
    /// The get enumerator.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    public IEnumerator<BaseItem> GetEnumerator()
    {
        return this.backpack.GetEnumerator();
    }

    /// <summary>
    /// The get enumerator.
    /// </summary>
    /// <returns>
    /// The <see cref="IEnumerator"/>.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// The add item.
    /// </summary>
    /// <param name="item">
    /// The item.
    /// </param>
    public void AddItem(BaseItem item)
    {
        this.backpack.Add(item);
    }

    /// <summary>
    /// Remove an item from the backpack.EquippableItem
    /// </summary>
    /// <param name="item">
    /// The item to remove
    /// </param>
    public void RemoveItem(BaseItem item)
    {
        this.backpack.Remove(item);
    }

    // TODO allow for consumable items
    public void EquipItem(BaseItem item, int slot)
    {
        // Weapon
        if (slot == 0)
        {
            Assert.IsTrue(item.Type == BaseItem.ItemType.Equipment, "Illegal attempt to equip non-equippable item to an equipment slot");

            // Put old weapon into backpack
            if (this.equippedWeapon != null)
            {
                this.equippedWeapon.UnEquip();
                this.AddItem(this.equippedWeapon);
            }
            this.equippedWeapon = item as EquippableItem;
            this.equippedWeapon.Equip();
        }

        else
        {
            if (this.equippedMisc[slot - 1] != null)
            {
                var realOldItem = this.equippedMisc[slot - 1] as EquippableItem; //Temp
                realOldItem.UnEquip();
                this.AddItem(this.equippedMisc[slot - 1]);
            }

            var realItem = item as EquippableItem;
            this.equippedMisc[slot - 1] = realItem;
            realItem.Equip();
        }
    }

    public void UnequipItem(EquippableItem item)
    {
        
    }

    public bool CanEquip(BaseItem item, int slot)
    {
        // TODO: Check against slot type instead of slot number?
        if (slot == 0)
        {
            // Check if equipment
            if (item.Type != BaseItem.ItemType.Equipment)
            {
                return false;
            }

            // Check if weapon
            return ((EquippableItem) item).TypeOfEquipment == EquippableItem.EquipmentType.Weapon;
        }
        if (slot > 0 && slot <=3)
        {
            // Check if equipment
            if (item.Type != BaseItem.ItemType.Equipment)
            {
                return false;
            }

            // Check if misc 
            return ((EquippableItem) item).TypeOfEquipment == EquippableItem.EquipmentType.Misc;
        }

        // Check if consumable
        return item.Type == BaseItem.ItemType.Consumable;
    }

    private void Start()
    {
        BaseItem item = ItemGenerator.GenerateItem(GeneratedItemType.Equipment);
        Debug.Log(item);
        Debug.Log(item.GetDescription(false));
    }


}