using UnityEngine;
using System.Collections;

/// <summary>
/// Selects the active skill for the player to this button's selected skill
/// </summary>
public class SkillUseButton : MonoBehaviour
{

    private GameObject _player;
    private SkillBehaviour _playerSkills;

    // The skill that should be used if this button is pressed when targeting
    public BaseSkill AssociatedSkill
    {
        get { return _playerSkills.StartingSkills[SkillIndex]; }
    }

    // The #n skill that should be used on player's SkillBehaviour when this is used
    public int SkillIndex;

    public void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _playerSkills = _player.GetComponent<SkillBehaviour>();
    }
}