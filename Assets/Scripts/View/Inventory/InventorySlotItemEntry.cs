namespace Assets.Scripts.View.Inventory
{
    using UnityEngine;

    public class InventorySlotItemEntry : InventoryItemEntry
    {
        public override void OnClicked()
        {
            GameObject.FindWithTag("InventoryManager").GetComponent<InventoryManager>().ClickEquipmentButton(this.ItemSlot);
        }
    }
}