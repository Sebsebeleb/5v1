using BaseClasses;
using Data.Skills;
using Data.Skills.Warrior;
using Scripts.View.LevelupMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

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


    // The levels that the player will be able to learn a new specialization
    private static readonly int[] SpecializationLevels = new[] { 2, 4, 6, 9 };

    // List specializations that exist. Kinda temp solution
    private Specialization[] Specializations =
                                           {
                                            new Specialization("Warlockery", 
@"Gain focus on <color=""purple"">Warlockery</color> skills
Whenever an enemy dies, heal for 0.5% max health for each debuff it had.", 
                                                "Warlockery",
                                                new Color(0.56f, 0.031f, 0.367f),
                                                BaseSkill.SkillCategory.Warlockery), 
                                            /*new Specialization("Elementalist",
@"Gain focus on <color=""red"">Fire</color>, <color=""blue"">Water</color> and <color=""yellow"">Lightning</color> skills.
Skills are 25% potent per elemental debuff on the target.", 
                                                "Elementalist",
                                                new Color(0.568f, 0.211f, 0.513f)), */
                                            new Specialization("Pyromancer",
@"Gain focus on <color=""red""> Fire</color> skills.
Gain +1 mana regen for each burning enemy",
                                                "Pyromancer",
                                                new Color(1, 0.43f, 0.227f),
                                                BaseSkill.SkillCategory.Fire),
                                            new Specialization("Hydromancer",
                                                @"Gain focus on <color=""blue"">Water</color> skills
The ""Wet"" debuff will now also make enemies 20% more vulnerable to spells.",
                                                "Toiletery",
                                                new Color(0.501f, 0.701f, 0.866f),
                                                BaseSkill.SkillCategory.Water), 
                                           };
    [Header("Specialization references")]
    [SerializeField]
    private GameObject SpecializationView;

    [SerializeField]
    private GameObject SpecializationEntryPrefab;

    [SerializeField]
    private Transform SpecializationEntries;

    private int playerLevel;

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

    private void OnPlayerLevel(int i)
    {

        this.playerLevel = i;

        //Check if the player should be able to learn a specialization
        if (SpecializationLevels.Contains(i))
        {
            InitSpecialization();
        }
        else
        {
            Init(i);
        }
    }

    /// <summary>
    /// Sets up the specialization window
    /// </summary>
    private void InitSpecialization()
    {
        this.SpecializationView.SetActive(true);

        // Cleanup old
        foreach (Transform child in this.SpecializationEntries.transform)
        {
            Destroy(child.gameObject);
        }

        // Initialize specializations
        foreach (Specialization s in this.Specializations)
        {
            GameObject entry = Instantiate(SpecializationEntryPrefab);
            entry.transform.SetParent(this.SpecializationEntries);

            SpecializationEntryBehaviour behaviour = entry.GetComponent<SpecializationEntryBehaviour>();

            behaviour.SetSpecialization(s);
            entry.GetComponent<Toggle>().group = this.SpecializationEntries.GetComponent<ToggleGroup>();

        }

    }

    private void Init(int i)
    {
        BaseSkill[] skills = this.GenerateChoices(i);

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

    private BaseSkill[] GenerateChoices(int i)
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
            new HungeringStrike(i),

        };

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

        // Find a focused skill

        BaseSkill focus = RollFocusSkill(finalChoices.ToArray());


        if (focus != null)
        {
            finalChoices.Remove(focus);
            finalChoices.Insert(0, focus);
        }


        int choices = 4;

        int max = Math.Min(choices, finalChoices.Count);

        return finalChoices.GetRange(0, max).ToArray();

    }

    private BaseSkill RollFocusSkill(BaseSkill[] choices)
    {
        // Find a focused skill
        if (this.PlayerSkills.GetKnownSpecializations().Length != 0)
        {

            List <BaseSkill.SkillCategory> knownCategories = new List<BaseSkill.SkillCategory>();


            foreach (Specialization spec in this.PlayerSkills.GetKnownSpecializations())
            {
                knownCategories.Add(spec.Focus);
            }

            // Check if it has any of the categories the player knows
            foreach (BaseSkill skill in choices)
            {
                // TODO: Convert to bitwise check (Or maybe not? seems like Contains might do it for us?)
                if (knownCategories.Contains(skill.Category))
                {
                    return skill;
                }
            }
        }

        return null;
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

    public void ConfirmSpecialization()
    {

        ToggleGroup v = this.SpecializationEntries.GetComponent<ToggleGroup>();

        // No choice selected
        if (!v.AnyTogglesOn())
        {
            return;
        }

        Toggle choice = v.ActiveToggles().ToList()[0];

        SpecializationEntryBehaviour behaviour = choice.GetComponent<SpecializationEntryBehaviour>();

        Specialization a = behaviour.GetSpecialization();

        this.PlayerSkills.LearnSpecialization(a);

        this.SpecializationView.SetActive(false);

        this.Init(this.playerLevel);
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