using BaseClasses;

namespace Data.Effects
{

	// Boosted by a skelton's SkeletonGrowth. Basically attack bonus
    [System.Serializable]
    public class Boosted : Effect
    {


        private int bonus;



        // An empty constructor has to be provided for serialization
        public Boosted(){
            IsBuff = true;
            bonus = 0;

            Description = new EffectDescription("Boosted", () => "Attack increased by " + bonus);

        }

        public Boosted(int attackBonus) : base()
        {
            IsBuff = true;
            bonus = attackBonus;

            Description = new EffectDescription("Boosted", () => "Attack increased by " + bonus);
        }



        public override void OnAdded()
        {
            base.OnAdded();

            owner.attack.BonusAttack += bonus;
        }

        public override void OnRemoved()
        {
            base.OnRemoved();

            owner.attack.BonusAttack -= bonus;
        }

        public override bool ShouldAnimate()
        {
            return true;
        }

        public override ChangeAnimation GetAnimationInfo()
        {
            ChangeAnimation a = new ChangeAnimation();
            a.SpawnHoverText = true;
            a.IconName = "Boosted";

            return a;
        }
    }
}