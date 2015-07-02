
// A simple generic attack buff TODO: Remame into priestly buff due to aniamtins/tooltips
using BaseClasses;

class AttackBuff : Effect
{
    
    
    private int bonus;

    
    // description: Pass it to describe the way it was buffed
    public AttackBuff(int attackBonus) : base(){
        bonus = attackBonus;
        
        
        Description = new EffectDescription("Priestly buff", () => "Attack increased by " + bonus);
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

    public override bool ShouldAnimate(){
        return true;
    }
    
    public override ChangeAnimation GetAnimationInfo(){
        ChangeAnimation a = new ChangeAnimation();
        a.SpawnHoverText = true;
        a.IconName = "Cross";
        
        return a;
    }
    

}
