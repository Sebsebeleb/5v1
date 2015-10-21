using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

using Data.Skills;
public class LevelupManagerBehaviour : MonoBehaviour
{
    public GameObject SkillEntryPrefab;
    public SkillBehaviour PlayerSkills;
    public GameObject RealHolder; // Hacky way to enable/disable a menu on start

    [Header("New Skills references")]
    // Transform and groups for new skills we can learn
    public Transform SkillsParent;
    public ToggleGroup LearnableSkillsToggles;
    public Text TitleLearnableSkill; // The title for the description of the learnable skill you have selected
    public Text DescriptionLearnableSkill;

    [Header("Old Skills references")]
    // The transform and groups for the skills the player already knows. Used for updating description and checking what skill to replace
    public Transform KnownSkillsParent;
    public ToggleGroup KnownSkillsToggles;
    public Text TitleKnownSkill;
    public Text DescriptionKnownSkill;

    void Awake()
    {
        //delegateeventCallback = OnPlayerLevel;
        Event.PlayerLeveledUp callback = OnPlayerLevel;
        Event.EventManager.Register(Event.Events.PlayerLeveledUp, callback);
    }

    void Start()
    {
        RealHolder.SetActive(true);
        RealHolder.SetActive(false);
    }

    // notused parameter is a lazy as fuck way of dealing with the event manager.
    private void OnPlayerLevel(int i)
    {
        List<BaseSkill> skills = new List<BaseSkill>(){
            new Block(i),
            new Bloodlust(i),
            new Cleave(i),
            new Stun(i),
            new ArcLightning(i),
            new Blizzard(i),
            new Switcheroo(i),
            new LightningBolt(i),
            new FireShield(i),
            new WildFire(i),
            new Inferno(i),
            new ConsumingFire(i),
            new FrostFire(i),
            new HolyFire(i),

        };

        BaseSkill blockSkill = new Data.Skills.Block(i);

        // Generate a choice of 4 skills for the player to levelup
        List<BaseSkill> finalChoices = new List<BaseSkill>(skills);

        foreach (BaseSkill skill in skills)
        {
            if (skill.Rank < 0 ||
                PlayerSkills.KnowsSkill(skill)
                )
            {
                finalChoices.Remove(skill);
            }
        }
        finalChoices.Shuffle();

        int max = Math.Min(4, finalChoices.Count);

        Init(finalChoices.GetRange(0, max).ToArray());
    }

    public void Init(BaseSkill[] skills)
    {
        RealHolder.SetActive(true);

        // Cleanup old
        foreach (Transform child in SkillsParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(Transform child in KnownSkillsParent.transform)
        {
            Destroy(child.gameObject);
        }

        // Initalize learnable skills
        foreach (BaseSkill skill in skills)
        {
            GameObject entry = Instantiate(SkillEntryPrefab) as GameObject;
            entry.transform.SetParent(SkillsParent);

            LearnableSkillBehaviour behaviour = entry.GetComponent<LearnableSkillBehaviour>();
            behaviour.SetSkill(skill);

            behaviour.GetComponentInChildren<Toggle>().group = LearnableSkillsToggles.GetComponent<ToggleGroup>();
        }

        // Initalize old known skills
        foreach(BaseSkill skill in PlayerSkills.GetKnownSkills())
        {
            GameObject entry = Instantiate(SkillEntryPrefab) as GameObject;
            entry.transform.SetParent(KnownSkillsParent);

            LearnableSkillBehaviour behaviour = entry.GetComponent<LearnableSkillBehaviour>();
            behaviour.IsOldSkill = true;
            if (skill != null)
            {
               behaviour.SetSkill(skill);
            }

            behaviour.GetComponentInChildren<Toggle>().group = KnownSkillsToggles.GetComponentInParent<ToggleGroup> ();
        }
    }

    public void ConfirmLevelup()
    {

        // Check if the player has selected a skill slot
        if (!KnownSkillsToggles.AnyTogglesOn()) return;

        if (!LearnableSkillsToggles.AnyTogglesOn()) return;

        // Check what skill the player selected
        BaseSkill skill = null;

        Toggle[] toggles = SkillsParent.GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
            {
                skill = toggle.GetComponentInParent<LearnableSkillBehaviour>().skill;
                break;
            }
        }

        // Find the index of the slot that we want to override
        int slotIndex = 0;
        Toggle[] slots = KnownSkillsToggles.GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in slots)
        {
            if (toggle.isOn)
            {
                break;
            }

            slotIndex++;
        }

        Assert.IsNotNull<BaseSkill>(skill, "For some reason, a toggle with a skill is active but no skill was found");

        PlayerSkills.LearnSkillInSlot(skill, slotIndex);

        // When there are no more skills to learn, let the player just exit regardless
        RealHolder.SetActive(false);
    }



    /// <summary>
    /// Called when the player selects or deselects a learnable skill
    /// </summary>
    public void SetSelectedNewSkill(BaseSkill skill)
    {
        string title = string.Format("{0} ({1})", skill.GetName(), skill.Rank);
        string desc = TextUtilities.ImproveText(skill.GetTooltip());

        TitleLearnableSkill.text = title;
        DescriptionLearnableSkill.text = desc;
    }

    /// <summary>
    /// Called when the player selects or deselects an already known skill
    /// </summary>
    public void SetSelectedOldSkill(BaseSkill skill)
    {
        string title = string.Format("{0} ({1})", skill.GetName(), skill.Rank);
        string desc = TextUtilities.ImproveText(skill.GetTooltip());

        TitleKnownSkill.text = title;
        DescriptionKnownSkill.text = desc;
    }
    
    /// <summary>
    /// No parameters meaning no skill in that slot
    /// </summary>
    public void SetSelectedOldSkill()
    {
        TitleKnownSkill.text = "...";
        DescriptionKnownSkill.text = "";
    }
}