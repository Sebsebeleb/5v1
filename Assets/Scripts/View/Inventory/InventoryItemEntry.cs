namespace Assets.Scripts.View.Inventory
{
    using UnityEngine;
    using UnityEngine.UI;

    public abstract class InventoryItemEntry : MonoBehaviour
    {

        [Header("References")]
        [SerializeField]
        protected Image ItemTypeIcon;

        [SerializeField]
        protected Text ItemName;

        [SerializeField]
        protected Text ItemDescription;

        // For changing the background colour based on item type
        [SerializeField]
        protected Image background;

        // Is this button part of the backpack display or the equipped items
        [SerializeField]
        protected bool IsInventoryButton;
        
        [Header("Stuff that matters")]
        // The item in the slot
        public BaseItem Item;

        // 0 = weapon, 1-3 = misc equipment etc.
        [Tooltip("The inventory slot this entry referes to")]
        public int ItemSlot;



        public void SetItem(BaseItem item)
        {
            this.Item = item;
            Debug.Log(item.Type);

            // Setup item type specific stuff
            if (item.Type == BaseItem.ItemType.Equipment)
            {
                EquippableItem equipItem = item as EquippableItem;

                Debug.Log(equipItem.TypeOfEquipment);
                if (equipItem.TypeOfEquipment == EquippableItem.EquipmentType.Weapon)
                {
                    Debug.Log("Setting to weapon");
                    Debug.Log(Colors.InventoryWeaponColor);
                    
                    background.color = Colors.InventoryWeaponColor;
                }
                else
                {
                    Debug.Log("Setting to misc");
                    Debug.Log(Colors.InventoryMiscColor);

                    background.color = Colors.InventoryMiscColor;
                }
            }
            else
            {
                //TODO this
            }

            // Setup name and description

            ItemName.text = item.Name;

            ItemDescription.text = item.GetDescription(true);
        }

        public abstract void OnClicked();
    }
}