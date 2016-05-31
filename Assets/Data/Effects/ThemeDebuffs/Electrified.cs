namespace BBG.Data.Effects.ThemeDebuffs
{
    using BBG.BaseClasses;
    using BBG.View;

    [System.Serializable]
    public class Electrified : Effect
    {
        public Electrified()
        {
            this.IsDebuff = true;
            this.IsThemeDebuff = true;

            this.Description = new EffectDescription("Electrified", () => "This enemy is electrified, it has -1 attack and arc lighting will not jump to it");
        }

        public override void OnAdded()
        {
            base.OnAdded();

            this.owner.attack.BonusAttack -= 1;
        }

        public override void OnRemoved()
        {
            base.OnRemoved();

            this.owner.attack.BonusAttack += 1;
        }

        public override bool ShouldAnimate()
        {
            return true;
        }

        public override ChangeAnimation GetAnimationInfo()
        {
            ChangeAnimation a = new ChangeAnimation();
            a.SpawnHoverText = true;
            a.IconName = "Electrified";

            return a;
        }
    }
}