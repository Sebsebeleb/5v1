using System;
using BaseClasses;
using System.Collections.Generic;
using Data.Skills;
using UnityEngine;

public class SkillBehaviour : MonoBehaviour
{
	private BaseSkill[] _knownSkills = new BaseSkill[4];

    void Start()
    {
        LearnSkill(new Bloodlust());
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

	public BaseSkill[] GetKnownSkills(){
		return _knownSkills;
	}

    // TODO: Cleanup shitty code
    public bool KnowsSkill(BaseSkill skill){
        for (int i = 0; i < 4; i++){
            if (_knownSkills[i] == null){
                return false;
            }
            if (_knownSkills[i].GetType() == skill.GetType()){
                return true;
            }
        }
        return false;
    }
}