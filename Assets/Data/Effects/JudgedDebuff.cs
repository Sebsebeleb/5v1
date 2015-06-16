using BaseClasses;

internal class Judged : Effect
{
    public Judged(int duration)
        : base(duration)
    {
        Description = new EffectDescription(
            "Judged",
            describe
        );
    }
    
    private string describe(){
        return "You cannot use skills!";
    }

    public override void OnAdded()
    {
        base.OnAdded();

        owner.status.SetSilenced(true);
    }

    public override void OnRemoved()
    {
        base.OnRemoved();

        owner.status.SetSilenced(false);
    }
}