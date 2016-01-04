using Map;
using System;
using System.Collections.Generic;

public abstract class BaseSkill : ITooltip, IRankCalculatable
{
    [Flags]
    public enum SkillCategory
    {
        Fire,
        Water,
        Lightning,
        Warrior,
        Warlockery,
    }
    // Indices = rank level, value = min level/max level to get it. (example, if RankLevelMin = [1, 4, 6] and RankLevelMax = [2, 6, 8] the player can only get rank 1 from level 1-2, rank 2 from 4-6 and rank 3 from 6-8
    public static readonly int[] RankLevelMin = new int[5]{0, 3, 5, 7, 9};
    public static readonly int[] RankLevelMax = new int[5]{4, 6, 8, 11, int.MaxValue};

    public string SkillName = "Undefined";
    public string Tooltip = "Undefined";
    public int Level;

    public int ManaCost;

    public int BaseCooldown;
    public int CurrentCooldown;

    // The rank of the skill, higher ranks require higher player level
    public int Rank;

    private SkillCategory category;

    public SkillCategory Category
    {
        get
        {
            return category;
        }

        protected set
        {
            category = value;
        }
    }

    //Player level is the level used to determine rank given to this skill.
    public BaseSkill(int PlayerLevel)
    {
        CurrentCooldown = BaseCooldown;
        Rank = GetRank(PlayerLevel);
    }

    
    /// <summary>
    /// Can we target this position?
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public virtual bool CanTargetGrid(int x, int y)
    {
        GridPosition pos = new GridPosition(x, y);
        
        return this.GetValidTargets().Contains(pos);
    }

    /// <summary>
    /// Returns all grid positions this skill can be used on
    /// </summary>
    /// <returns>List of valid targets</returns>
    public virtual List<GridPosition> GetValidTargets()
    {
        return Targeting.Targets.NotCorpses();
    }

    public virtual void UseOnTargetGrid(int x, int y)
    {
        Actor.Player.CurrentMana -= this.ManaCost;
        this.CurrentCooldown = this.BaseCooldown;

        if (!this.CanTargetGrid(x, y))
        {
            throw new Exception("UUUh something went wrong with spell casting, targeting wasnt checked properly");
        }

    }

    public virtual bool CanUse(int x, int y)
    {
        // Check mana cost
        if (this.ManaCost != -1 && this.ManaCost > Actor.Player.CurrentMana)
        {
            return false;
        }

        if (this.CurrentCooldown > 0)
        {
            return false;
        }

        return true;
    }

    // *IRankCalculatable*

    /// <summary>
    /// A rank is given based on the difficulty given by the level of the player.
    /// Skills have a min level and a max level for each rank that the player can get them at.
    /// -1 means no rank found
    /// </summary>
    /// <param name="playerLevel">The level of the player to roll the rank on</param>
    /// <returns>The rank the skill will have</returns>
    public int GetRank(int playerLevel)
    {
        List<int> possibleRanks = new List<int>();

        for (int i = 0; i < RankLevelMin.Length; i++)
        {
            if (RankLevelMin[i] <= playerLevel && RankLevelMax[i] >= playerLevel)
            {
                possibleRanks.Add(i + 1);
            }
        }

        if (possibleRanks.Count == 0)
        {
            return -1;
        }

        int result = possibleRanks.RandomElement();

        return result;
    }

    // *ITooltip*
    public virtual string GetName()
    {
        return this.SkillName;
    }

    public abstract string GetTooltip();

}