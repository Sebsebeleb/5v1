using System;

public class BaseSkill
{

    public string SkillName = "Undefined";
    public string Tooltip = "Undefined";

    public virtual bool CanTargetGrid(int x, int y)
    {
        return true;
    }

    public virtual void UseOnTargetGrid(int x, int y)
    {
        if (!CanTargetGrid(x, y))
        {
            throw new Exception("UUUh something went wrong with spell casting, targeting wasnt checked properly");
        }
    }
}