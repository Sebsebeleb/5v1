using BaseClasses;

namespace Data.Effects
{
    public class Electrified : Effect
    {
        public Electrified()
        {
            IsDebuff = true;
            IsThemeDebuff = true;

            Description = new EffectDescription("Electrified", () => "This enemy is electrified, it has -1 attack and arc lighting will not jump to it");
        }

        public override void OnAdded()
        {
            base.OnAdded();

            owner.attack.BonusAttack -= 1;
        }

        public override void OnRemoved()
        {
            base.OnRemoved();

            owner.attack.BonusAttack += 1;
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