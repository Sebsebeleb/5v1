using UnityEngine;

using Data.Effects;

namespace Data.Skills
{
    [System.Serializable]
    class FrostFire : BaseSkill
    {
        public FrostFire(int PlayerLevel) : base(PlayerLevel)
        {
            SkillName = "Frost Fire";
            Tooltip = "Deal {0} damage to an enemy. if the enemy was wet, apply stun {1}, remove wet, and then apply burning. If enemy was burning, consume burning, deal 150% of remaining burning damage and apply wet.";
            BaseCooldown = 10;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

			Actor target = GridManager.TileMap.GetAt(x, y);

			target.damagable.TakeDamage(getMainDamage());

            bool wasWet = target.effects.HasEffect<Wet>();
            bool wasHot = target.effects.HasEffect<Burning>();

            // Wet route ;^)
            if (wasWet){
                target.effects.AddEffect(new Stunned(getStun()));
                target.effects.AddEffect(new Burning(5));
            }

            // Hot route ;-)
            else if (wasHot){
                int burnDuration = target.effects.GetEffectsOfType<Burning>()[0].Duration;
                target.damagable.TakeDamage((int) (burnDuration * 1.5f));

                var burningEffect = target.effects.GetEffectsOfType<Burning>()[0];
                target.effects.RemoveEffect(burningEffect);
                target.effects.AddEffect(new Wet(5));
            }

            // Bad ending :(
            else {

            }
        }

        private int getStun(){
            return 1 + (int) Rank / 3;
        }

        private int getMainDamage(){
            return 2 + Rank*2;
        }

        public override string GetTooltip(){

            string mainDamageProp = TextUtilities.FontColor(Colors.DamageValue, getMainDamage().ToString());
            string stunDurationProp = TextUtilities.FontColor(Colors.DurationValue, getStun().ToString());


            return string.Format(Tooltip, mainDamageProp, stunDurationProp);
        }
    }
}
