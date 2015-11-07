
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
}