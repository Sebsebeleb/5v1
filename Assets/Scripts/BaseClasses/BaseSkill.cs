using System;
using System.Collections.Generic;

using UnityEngine;

public abstract class BaseSkill : ITooltip, IRankCalculatable
{

    public string SkillName = "Undefined";
    public string Tooltip = "Undefined";
    public int Level;

    public int BaseCooldown;
    public int CurrentCooldown;

    // The rank of the skill, higher ranks require higher player level
    public int Rank;

    // Indices = rank level, value = min level/max level to get it. (example, if RankLevelMin = [1, 4, 6] and RankLevelMax = [2, 6, 8] the player can only get rank 1 from level 1-2, rank 2 from 4-6 and rank 3 from 6-8
    public static readonly int[] RankLevelMin = new int[5]{0, 3, 5, 7, 9};
    public static readonly int[] RankLevelMax = new int[5]{4, 6, 8, 11, int.MaxValue};

    //Player level is the level used to determine rank given to this skill.
    public BaseSkill(int PlayerLevel)
    {
        CurrentCooldown = BaseCooldown;
        Rank = GetRank(PlayerLevel);
    }

    public bool CanTargetGrid(int x, int y)
    {
        return true;
    }

    public virtual void UseOnTargetGrid(int x, int y)
    {
        this.CurrentCooldown = this.BaseCooldown;

        if (!CanTargetGrid(x, y))
        {
            throw new Exception("UUUh something went wrong with spell casting, targeting wasnt checked properly");
        }

    }

    public bool CanUse(int x, int y)
    {
        return CurrentCooldown <= 0;
    }

    /// IRankCalculatable
    // A rank is given based on the difficulty given by the level of the player.
    //Skills have a min level and a max level for each rank that the player can get them at.
    // -1 means no rank found
    public int GetRank(int PlayerLevel)
    {

        //{1, 3, 5, 7, 9};
        //{4, 6, 8, 11, int.MaxValue};
        List<int> PossibleRanks = new List<int>();

        for (int i = 0; i < RankLevelMin.Length; i++)
        {
            if (RankLevelMin[i] <= PlayerLevel && RankLevelMax[i] >= PlayerLevel)
            {
                    PossibleRanks.Add(i+1);
            }
        }

        if (PossibleRanks.Count == 0){
            return -1;
        }

        int result = PossibleRanks.RandomElement();
        
        return result;
    }

    ///ITooltip

    public virtual string GetName()
    {
        return SkillName;
    }

    public abstract string GetTooltip();

}