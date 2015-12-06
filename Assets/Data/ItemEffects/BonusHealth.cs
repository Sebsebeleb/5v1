namespace Assets.Data.ItemEffects
{
    class BonusHealth : ItemEffect
    {

        private readonly int bonusHealth;

        public BonusHealth(int bonus_health)
        {
            this.bonusHealth = bonus_health;
        }
        public override string GetDescription(BaseItem item, bool richText)
        {
            string desc = "Deal {0} to enemies that attack you";

            if (richText)
            {
                desc = string.Format(desc, TextUtilities.FontColor(Colors.DamageValue, this.bonusHealth));
            }
            else
            {
                desc = string.Format(desc, this.bonusHealth);
            }

            return desc;
        }

        public override void Equipped(BaseItem item, Actor wearer)
        {
            wearer.damagable.BonusMaxHealth += this.bonusHealth;
        }

        public override void UnEquipped(BaseItem item, Actor wearer)
        {
            wearer.damagable.BonusMaxHealth -= this.bonusHealth;
        }
    }
}
