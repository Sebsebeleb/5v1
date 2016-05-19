namespace BBG.Items
{
    [System.Serializable]
    public class ConsumableItem : BaseItem
    {

        public ConsumableItem()
        {
            this.UseType = ItemUseType.Consumable;
        
        }
    }
}