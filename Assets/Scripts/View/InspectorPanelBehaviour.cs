using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

using BaseClasses;

public class InspectorPanelBehaviour : MonoBehaviour
{
    public GameObject ActionItemPrefab;

    public Text ActorName;
    public Text ActorDescription;
    public Text ActorHPText;
    public Text ActorAttackText;
    public Text ActorCooldownText;

    public Transform ActionsParent;
    public Transform EffectsParent;
    // The panel that can display detailed information about actions and effects
    public Transform DescriptionPanel;

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
        ActorName.text = who.name;
        ActorDescription.text = "Not yet implemented";
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
        if (who.GetComponent<SkillBehaviour>() != null)
        {
            PopulateSkills(who);
        }
    }

    //For the player's skills
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
    private void CreateEntry(ITooltip entryData, Transform holder)
    {

        GameObject item = Instantiate(ActionItemPrefab) as GameObject;

        item.transform.SetParent(holder);

        // TODO: this is a temp solution
        item.GetComponentInChildren<Text>().text = entryData.GetName();

        // Store a reference so we can retrieve it later if we want to get the description
        _entries.Add(item.transform, entryData);
    }

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
        /// TODO: Temp solution
        foreach (Transform child in EffectsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Effect eff in who.GetComponent<EffectHolder>())
        {
            if (eff.IsTrait)
            {
                CreateEntry(eff, ActionsParent);
            }
            else
            {
                CreateEntry(eff, EffectsParent);
            }
        }
    }
}

