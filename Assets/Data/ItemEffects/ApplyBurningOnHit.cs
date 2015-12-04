using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Data.ItemEffects
{
    using Event;

    using global::Data.Effects;

    using UnityEngine.Experimental.Networking;

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

        public override void Equipped(BaseItem item)
        {
            EventManager.Register(
                Events.PlayerAttackCommand,
                (ActorParameters)this.DoBurning);
        }

        public override void UnEquipped(BaseItem item)
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
