
// A simple generic attack buff
using BaseClasses;

class Stunned : Effect
{
    

    public Stunned(int duration) : base(duration)
    {
        Description = new EffectDescription("Stunned",
            describe);
    }
    
    private string describe(){
        return "This enemy is stunned, it does not have it's countdown reduced";
    }
    
    public override void OnAdded()
    {
        base.OnAdded();

        owner.status.SetStunned(true);
    }

    public override void OnRemoved()
    {
        base.OnRemoved();
		
		owner.status.SetStunned(false);
    }
}
