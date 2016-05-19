namespace BBG.Data.ItemEffects
{
    using BBG.Actor;
    using BBG.Items;

    class Thorny : ItemEffect
    {

        private readonly int reflectDamage;

        public Thorny(int damage)
        {
            this.reflectDamage = damage;
        }
        public override string GetDescription(BaseItem item, bool richText)
        {
            string desc = "Deal {0} to enemies that attack you";

            if (richText)
            {
                desc = string.Format(desc, TextUtilities.FontColor(Colors.DamageValue, this.reflectDamage));
            }
            else
            {
                desc = string.Format(desc, this.reflectDamage);
            }

            return desc;
        }

        public override void Equipped(BaseItem item, Actor wearer)
        {
            EventManager.Register(Events.EnemyAttack, (OnEnemyAttack)this.DoReflect);
        }

        public override void UnEquipped(BaseItem item, Actor wearer)
        {
            EventManager.UnRegister(Events.EnemyAttack, (OnEnemyAttack)this.DoReflect);
        }

        private void DoReflect(EnemyAttackArgs args)
        {
            args.who.damagable.TakeDamage(this.reflectDamage);
        }
    }
}
