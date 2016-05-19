
// A simple generic attack buff TODO: Remame into priestly buff due to aniamtins/tooltips

namespace BBG.Data.Effects
{
    using BBG.BaseClasses;
    using BBG.View;

    [System.Serializable]
    public class AttackBuff : Effect
    {


        private int bonus;



        // An empty constructor has to be provided for serialization
        public AttackBuff(){
            this.IsBuff = true;
            this.bonus = 0;

            this.Description = new EffectDescription("Attack buff", () => "Attack increased by " + this.bonus);

        }

        public AttackBuff(int attackBonus) : base()
        {
            this.IsBuff = true;  
            this.bonus = attackBonus;

            this.Description = new EffectDescription("Attack buff", () => "Attack increased by " + this.bonus);
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
            a.IconName = "Cross";

            return a;
        }
    }
}