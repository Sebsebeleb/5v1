using BaseClasses;

internal class Blocking : Effect
{
    public Blocking(int duration)
        : base(duration)
    {
        Description = new EffectDescription(
            "Blocking",
            describe
        );
    }
    
    private string describe(){
        return "You are invulnerable!";
    }

    public override void OnAdded()
    {
        base.OnAdded();

        owner.damagable.IsInvulnerable += 1;
    }

    public override void OnRemoved()
    {
        base.OnRemoved();

        owner.damagable.IsInvulnerable -= 1;
    }
}