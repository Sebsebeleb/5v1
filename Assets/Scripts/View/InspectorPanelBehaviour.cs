using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

using BaseClasses;
using DG.Tweening;

public class InspectorPanelBehaviour : MonoBehaviour
{
    public GameObject ActionItemPrefab;

    public Text ActorName;
    public Text ActorDescription;
    public Text ActorHPText;
    public Text ActorAttackText;
    public Text ActorCooldownText;

    // The entries are added as childs to these gameobjects, depending on the type of entry
    public Transform ActionsParent;
    public Transform EffectsParent;
    public Transform TraitsParent;

    // When the player is being inspected, we rename the title to "Skills" rather than "Actions"
    public Text ActionsHeadline;

    // The panel that can display detailed information about actions and effects
    public Transform DescriptionPanel;

    // Colors
    public Color EffectTraitColor; // The color for effects that are defined as traits
    public Color EffectThemedColor; // The color for the "Theme" debuffs like wet/burning/electrified
    public Color BuffColor; // Color for buffs
    public Color DebuffColor; // vs Debuffs

    // TODO: Refactor please. This should only need one interface, not two seperate lists
    private Dictionary<Transform, Effect> _effectEntries = new Dictionary<Transform, Effect>();
    private Dictionary<Transform, ITooltip> _entries = new Dictionary<Transform, ITooltip>();

    public void Update()
    {
        // Check if user wants to exit this screen
        if (Input.GetButton("Cancel"))
        {
            gameObject.SetActive(false);
        }
    }

    public void InspectActor(Actor who)
    {
        UpdateStats(who);
    }

    //Updates the basic stats
    private void UpdateStats(Actor who)
    {
        bool isPlayer = who.tag == "Player";

        ActorName.text = who.name;

        // Update description
        if (isPlayer)
        {
        }
        else
        {
            ActorDescription.text = who.GetComponent<DisplayData>().Description;
        }

        ActorHPText.text = string.Format("{0}/{1}", who.damagable.CurrentHealth, who.damagable.MaxHealth);
        ActorAttackText.text = who.attack.Attack.ToString();

        if (who.countdown != null)
        {
            ActorCooldownText.text = string.Format("{0}({1})", who.countdown.CurrentCountdown, who.countdown.MaxCountdown);
        }
        else
        {
            ActorCooldownText.text = "";
        }

        PopulateActions(who);
        PopulateEffects(who);

        // Update Actions title
        if (isPlayer)
        {
            ActionsHeadline.text = "skills";
        }
        else
        {
            ActionsHeadline.text = "actions";
        }

        // If we are inspecting a player, populate the skills into the Actions category
        if (isPlayer)
        {
            PopulateSkills(who);
        }
    }

    // For the player's skills
    private void PopulateSkills(Actor who)
    {
        if (who.tag != "Player")
        {
            Debug.LogWarning("Uuh, non-player has skills?");
        }
        SkillBehaviour playerSkills = who.GetComponent<SkillBehaviour>();

        foreach (BaseSkill skill in playerSkills.GetKnownSkills())
        {

            if (skill == null)
            {
                continue;
            }
            CreateEntry(skill, ActionsParent);
        }
    }

    private void PopulateActions(Actor who)
    {
        // TODO: Also temp solutuion, reset the gameobjects.
        foreach (Transform child in ActionsParent)
        {
            Destroy(child.gameObject);
        }
        // End
        AI brain = who.GetComponent<AI>();

        if (!brain)
        {
            return;
        }

        foreach (AI.AiAction action in brain.GetStandardActions())
        {
            CreateEntry(action, ActionsParent);
        }

        foreach (AI.AiAction freeAction in brain.GetFreeActions())
        {
            CreateEntry(freeAction, ActionsParent);
        }

    }



    // Holder is the transform it should be the child of
    // If duration != -1, it will append that number to the title (so for example "burning" becomes "burning (3)"
    // TODO: Should probably not display duration and duration icon on actions/traits
    private void CreateEntry(ITooltip entryData, Transform holder, Color color, int duration=-1)
    {

        GameObject item = Instantiate(ActionItemPrefab) as GameObject;
        ActionEntryBehaviour behaviour = item.GetComponent<ActionEntryBehaviour>();

        item.transform.SetParent(holder);
        item.GetComponent<Image>().color = color;

        // TODO: Hmm, not sure if I like having the title Improved() or not..
        string title = TextUtilities.ImproveText(entryData.GetName());

        // TODO: The infinity symbol in the current font is kinda weird and misplaced. So solution would be to use an icon in that case
        behaviour.DurationText.text = duration == -1 ? "∞" : duration.ToString();

        behaviour.TitleText.text = title;

        // Store a reference so we can retrieve it later if we want to get the description
        _entries.Add(item.transform, entryData);
    }

    private void CreateEntry(ITooltip entryData, Transform holder)
    {
        CreateEntry(entryData, holder, Color.white);
    }

    // When an entry is clicked, we popup a dialogue with the description for the skill
    public void DisplayDescriptionForEntry(Transform entry)
    {
        // TODO: Refactor pls
        ITooltip data = _entries[entry];

        string prettified = TextUtilities.ImproveText(data.GetTooltip());
        DisplayDescriptionOf(prettified);
    }

    private void DisplayDescriptionOf(string description)
    {
        DescriptionPanel.gameObject.SetActive(true);
        DescriptionPanel.GetComponentInChildren<Text>().text = description;
    }

    private void PopulateEffects(Actor who)
    {

        /// Delete old entries
        foreach (Transform child in EffectsParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in TraitsParent){
            Destroy(child.gameObject);
        }

        EffectHolder effectHolder = who.GetComponent<EffectHolder>();

        // TODO:  If there are no effects, we hide the category


        // Sort it by: Is it a buff? is it a debuff? its duration, then finally by name
        var effects = effectHolder.GetEffects();

        IEnumerable<Effect> sorted = effects.OrderBy(eff => eff.IsInfinite).
            ThenBy(eff => eff.Duration).
            ThenBy(eff => !eff.IsDebuff).
            ThenBy(eff => !eff.IsBuff).
            ThenBy(eff => eff.GetName()
            .Reverse());


        foreach (Effect eff in sorted)
        {
            if (eff.IsHidden){
                continue;
            }

            Color color = Color.white;

            if (eff.IsBuff)
            {
                color = BuffColor;
            }
            else if (eff.IsDebuff)
            {
                color = DebuffColor;
            }


            if (!eff.IsInfinite){
                CreateEntry(eff, EffectsParent, color, eff.Duration);
            }
            else{
                CreateEntry(eff, EffectsParent, color);
            }


        }
        // TODO:  If there are no traits, we hide the category

        foreach (Effect eff in effectHolder.GetTraits()){
            if (eff.IsHidden){
                continue;
            }
            CreateEntry(eff, TraitsParent);
        }
    }
}

