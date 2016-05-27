using UnityEngine;

namespace BBG.View.Inventory
{
    using BBG.Actor;
    using BBG.Items;

    using UnityEngine.UI;

    using Debug = UnityEngine.Debug;

    public class InventoryManager : MonoBehaviour
    {
        // Display Entires for items in the backpack are added here
        [SerializeField]
        private Transform backpackObject;

        [SerializeField]
        private Transform equippedObject;

        private PlayerEquipment equipment;

        [SerializeField]
        private GameObject itemDisplayEntryPrefab;

        private BackpackItemEntry selectedBackpackItem;

        public void Awake()
        {
            this.equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>();
        }

        public void OnEnable()
        {
            this.Clear();
            this.PopulateChildren();
        }

        private void PopulateChildren()
        {
            foreach (var item in this.equipment)
            {
                this.CreateItem(item, this.backpackObject);
            }
        }

        private void CreateItem(BaseItem item, Transform parent)
        {
            GameObject entry = Instantiate(this.itemDisplayEntryPrefab) as GameObject;

            InventoryItemEntry entryBehaviour = entry.GetComponentInChildren<InventoryItemEntry>();
            entryBehaviour.SetItem(item);
            entry.transform.SetParent(parent, false);
            Toggle b = entry.GetComponentInChildren<Toggle>();
            b.onValueChanged.AddListener((isOn => this.SelectBackpackItem(entryBehaviour as BackpackItemEntry)));
        }

        private void Clear()
        {
            foreach (Transform child in this.backpackObject)
            {
                Destroy(child.gameObject);
            }
        }

        public void ClickEquipmentButton(int slot)
        {
            // Check that we actually have a selected item
            // TODO: Maybe change to actually use the toggle groups?
            if (this.selectedBackpackItem == null)
            {
                return;
            }

            BaseItem realItem = this.selectedBackpackItem.Item;

            // Check that the item is allowed in the specified slot
            // TODO: Convert hard-coded slots into flags on the UI entries
            if (!this.equipment.CanEquip(realItem, slot))
            {
                // TODO: Give feedback that this is an illegal choice
                return;
            }

            // Apply equipment modifications
            this.equipment.EquipItem(realItem as EquippableItem, slot);

            // Update display TODO: Super slow and silly method probably
            InventoryItemEntry entry = this.equippedObject
                .GetChild(slot)
                .GetComponentInChildren<InventorySlotItemEntry>();

            Debug.Log(this.equippedObject.GetChild(slot));
            Debug.Log(this.equippedObject.GetChild(slot).GetComponentInChildren<InventoryItemEntry>());
            entry.SetItem(this.selectedBackpackItem.Item);


            //equipment.RemoveItem(this.selectedBackpackItem.Item);

            Destroy(this.selectedBackpackItem.transform.parent.gameObject);

            this.selectedBackpackItem = null;
        }

        public void SelectBackpackItem(BackpackItemEntry entry)
        {
            this.selectedBackpackItem = entry;
        }
    }
}