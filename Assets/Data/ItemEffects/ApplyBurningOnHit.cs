namespace BBG.Data.ItemEffects
{
    using BBG.Actor;
    using BBG.Data.Effects.ThemeDebuffs;
    using BBG.Items;

    public class ApplyBurningOnHit : ItemEffect
    {

        public override string GetDescription(BaseItem item, bool richText)
        {
            string desc = "Apply Burning({0}) whenever you attack an enemy with a standard attack";

            if (richText)
            {
                desc = string.Format(desc, TextUtilities.FontColor(Colors.Burning, 3));
            }
            else
            {
                desc = string.Format(desc, 3);
            }

            return desc;
        }

        public override void Equipped(BaseItem item, Actor wearer)
        {
            EventManager.Register(
                Events.PlayerAttackCommand,
                (ActorParameters)this.DoBurning);
        }

        public override void UnEquipped(BaseItem item, Actor wearer)
        {
            EventManager.UnRegister(
                Events.PlayerAttackCommand,
                (ActorParameters)this.DoBurning);
        }

        // Applies the functionality
        private void DoBurning(Actor who)
        {
            who.effects.AddEffect(new Burning(3));
        }
    }
}
