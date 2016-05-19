namespace BBG.Data.Effects.Enemies
{
    using BBG.BaseClasses;
    using BBG.View;

    // Boosted by a skelton's SkeletonGrowth. Basically attack bonus
    [System.Serializable]
    public class Boosted : Effect
    {


        private int bonus;



        // An empty constructor has to be provided for serialization
        public Boosted(){
            this.IsBuff = true;
            this.bonus = 0;

            this.Description = new EffectDescription("Boosted", () => "Attack increased by " + this.bonus);

        }

        public Boosted(int attackBonus) : base()
        {
            this.IsBuff = true;
            this.bonus = attackBonus;

            this.Description = new EffectDescription("Boosted", () => "Attack increased by " + this.bonus);
        }



        public override void OnAdded()
        {
            base.OnAdded();

            this.owner.attack.BonusAttack += this.bonus;
        }

        public override void OnRemoved()
        {
            base.OnRemoved();

            this.owner.attack.BonusAttack -= this.bonus;
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