﻿using System;
using BaseClasses;
using System.Collections.Generic;
using Data.Skills;
using UnityEngine;

public class SkillBehaviour : MonoBehaviour
{
    private BaseSkill[] _knownSkills = new BaseSkill[4];

    void Start()
    {
        LearnSkill(new Bloodlust(1));
    }


    public void CountDown()
    {
        foreach (var skill in GetKnownSkills()) {
            if (skill == null) continue;
            skill.CurrentCooldown--;
            skill.CurrentCooldown = Mathf.Max(skill.CurrentCooldown, 0);
        }
    }


    public void LearnSkill(BaseSkill skill){
        for (int i = 0; i<4; i++){
            if (_knownSkills[i] == null){
                _knownSkills[i] = skill;
                return;
            }
        }
    }

    /// <summary>
    /// Teaches a new skill in a specific slot, overriding the old skill in that slot
    /// </summary>
    /// <param name="skill">Skill to learn</param>
    /// <param name="slot">Slot place/replace it in</param>
    public void LearnSkillInSlot(BaseSkill skill, int slot)
    {
        _knownSkills[slot] = skill;
    }

    public BaseSkill[] GetKnownSkills(){
        return _knownSkills;
    }

    // TODO: Cleanup shitty code
    // If we have the same skill AND rank, we know it
    public bool KnowsSkill(BaseSkill skill){
        for (int i = 0; i < 4; i++){
            if (_knownSkills[i] == null){
                return false;
            }
            if (_knownSkills[i].GetType() == skill.GetType() && _knownSkills[i].Rank == skill.Rank){
                return true;
            }
        }
        return false;
    }
}