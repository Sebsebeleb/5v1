
// A simple generic attack buff TODO: Remame into priestly buff due to aniamtins/tooltips
using BaseClasses;

namespace Data.Effects
{

    [System.Serializable]
    public class AttackBuff : Effect
    {


        private int bonus;



        // An empty constructor has to be provided for serialization
        public AttackBuff(){
            IsBuff = true;
            bonus = 0;

            Description = new EffectDescription("Attack buff", () => "Attack increased by " + bonus);

        }

        public AttackBuff(int attackBonus) : base()
        {
            IsBuff = true;  
            bonus = attackBonus;

            Description = new EffectDescription("Attack buff", () => "Attack increased by " + bonus);
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
            a.IconName = "Cross";

            return a;
        }
    }
}