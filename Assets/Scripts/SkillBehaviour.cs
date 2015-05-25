using Data.Skills;
using UnityEngine;

public class SkillBehaviour : MonoBehaviour
{
    public BaseSkill[] StartingSkills = new BaseSkill[4];

    void Start()
    {
        if (StartingSkills.Length > 4)
        {
            Debug.LogError("Too many items in starting skills");
        }

        //TODO: temp solution
        StartingSkills[0] = new Cleave();
        StartingSkills[1] = new Bloodlust();
    }


    public void CountDown()
    {
        foreach (var skill in StartingSkills) {
            if (skill == null) continue;
            skill.CurrentCooldown--;
            skill.CurrentCooldown = Mathf.Max(skill.CurrentCooldown, 0);
        }
    }
}