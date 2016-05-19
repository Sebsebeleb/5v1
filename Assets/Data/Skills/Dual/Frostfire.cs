
namespace BBG.Data.Skills.Dual
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects;
    using BBG.Data.Effects.ThemeDebuffs;

    [System.Serializable]
    class FrostFire : BaseSkill
    {
        public FrostFire(int PlayerLevel) : base(PlayerLevel)
        {
            this.Category = SkillCategory.Fire | SkillCategory.Water;

            this.SkillName = "Frost Fire";
            this.Tooltip = "Deal {0} damage to an enemy. if the enemy was wet, apply stun {1}, remove wet, and then apply burning. If enemy was burning, consume burning, deal 150% of remaining burning damage and apply wet.";
            this.BaseCooldown = 10;
            this.ManaCost = 15;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            Actor target = GridManager.TileMap.GetAt(x, y);

            target.damagable.TakeDamage(this.getMainDamage());

            bool wasWet = target.effects.HasEffect<Wet>();
            bool wasHot = target.effects.HasEffect<Burning>();

            // Wet route ;^)
            if (wasWet){
                target.effects.AddEffect(new Stunned(this.getStun()));
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
            return 1 + (int) this.Rank / 3;
        }

        private int getMainDamage(){
            return 2 + this.Rank*2;
        }

        public override string GetTooltip(){

            string mainDamageProp = TextUtilities.FontColor(Colors.DamageValue, this.getMainDamage().ToString());
            string stunDurationProp = TextUtilities.FontColor(Colors.DurationValue, this.getStun().ToString());


            return string.Format(this.Tooltip, mainDamageProp, stunDurationProp);
        }
    }
}
