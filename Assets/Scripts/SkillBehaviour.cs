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
    }
}