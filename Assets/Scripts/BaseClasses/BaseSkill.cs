using System;

public class BaseSkill
{

    public string SkillName = "Undefined";
    public string Tooltip = "Undefined";

    public int BaseCooldown;
    public int CurrentCooldown;

    public BaseSkill()
    {
        CurrentCooldown = BaseCooldown;
    }

    public virtual bool CanTargetGrid(int x, int y)
    {
        return true;
    }

    public virtual void UseOnTargetGrid(int x, int y)
    {
        this.CurrentCooldown = BaseCooldown;

        if (!CanTargetGrid(x, y))
        {
            throw new Exception("UUUh something went wrong with spell casting, targeting wasnt checked properly");
        }

    }

    public bool CanUse(int x, int y)
    {
        return CurrentCooldown <= 0;
    }
}