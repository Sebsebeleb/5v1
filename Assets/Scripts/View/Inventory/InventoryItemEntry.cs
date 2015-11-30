namespace Assets.Scripts.View.Inventory
{
    using UnityEngine;
    using UnityEngine.UI;

    public class InventoryItemEntry : MonoBehaviour
    {

        [SerializeField]
        private Image ItemTypeIcon;

        [SerializeField]
        private Text ItemName;

        [SerializeField]
        private Text ItemDescription;

        // For changing the background colour based on item type
        [SerializeField]
        private Image background;

        public void SetItem(BaseItem item)
        {
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
    }
}