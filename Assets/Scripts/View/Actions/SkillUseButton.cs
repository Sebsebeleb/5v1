using System.Net.Mime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Selects the active skill for the player to this button's selected skill
/// </summary>
public class SkillUseButton : MonoBehaviour
{
    public Image IconImage;
    public Text CooldownText;

    private GameObject _player;
    private SkillBehaviour _playerSkills;

    private BaseSkill _cachedSkill;

    // The skill that should be used if this button is pressed when targeting
    public BaseSkill AssociatedSkill
    {
        
        get { return _playerSkills.GetKnownSkills()[SkillIndex]; }
    }

    // The #n skill that should be used on player's SkillBehaviour when this is used
    public int SkillIndex;

    public void Awake()
    {
        _player = GameObject.FindWithTag("Player");
        _playerSkills = _player.GetComponent<SkillBehaviour>();
    }

    public void Update()
    {
        if (AssociatedSkill != null) {
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        if (AssociatedSkill.CurrentCooldown > 0) {
            CooldownText.text = AssociatedSkill.CurrentCooldown.ToString();
        }
        else {
            CooldownText.text = "";
        }
        if (AssociatedSkill != _cachedSkill) {
            UpdateIcon();
            _cachedSkill = AssociatedSkill;
        }
    }

    private void UpdateIcon()
    {
        // TODO: Lazy way but ok
        Sprite icon = Resources.Load<Sprite>(AssociatedSkill.SkillName);
        if (icon == null) {
            Debug.LogError("Warning, couldnt load icon for skill with name: " + AssociatedSkill.SkillName);
        }
        else {
            IconImage.sprite = icon;
        }
    }
}