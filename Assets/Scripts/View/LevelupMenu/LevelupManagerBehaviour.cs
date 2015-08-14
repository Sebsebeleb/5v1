using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelupManagerBehaviour : MonoBehaviour
{
	public Transform SkillsParent;
	public GameObject SkillEntryPrefab;
	public ToggleGroup Toggles;
	public SkillBehaviour PlayerSkills;
	public GameObject RealHolder; // Hacky way to enable/disable a menu on start

	void Awake(){
		//delegateeventCallback = OnPlayerLevel;
		Event.PlayerLeveledUp callback = OnPlayerLevel;
		Event.EventManager.Register(Event.Events.PlayerLeveledUp, callback);
	}

	void Start(){
		RealHolder.SetActive(true);
		RealHolder.SetActive(false);
	}

	// notused parameter is a lazy as fuck way of dealing with the event manager.
	private void OnPlayerLevel(int i){
		List<BaseSkill> skills = new List<BaseSkill>(){
			new Data.Skills.Block(),
			new Data.Skills.Bloodlust(),
			new Data.Skills.Cleave(),
			new Data.Skills.Stun(),
			new Data.Skills.ArcLighting(),
			new Data.Skills.Blizzard(),
			new Data.Skills.Switcheroo(),
		};

		// Generate a choice of 4 skills for the player to levelup
		List<BaseSkill> finalChoices = new List<BaseSkill>(skills);


		foreach(BaseSkill skill in skills){
			if (PlayerSkills.KnowsSkill(skill)){
				finalChoices.Remove(skill);
			}
		}
		finalChoices.Shuffle();

		int max = Math.Min(4, finalChoices.Count);

		Init(finalChoices.GetRange(0, max).ToArray());
	}

	public void Init(BaseSkill[] skills){
		RealHolder.SetActive(true);

		// Cleanup old
		foreach(Transform child in SkillsParent.transform){
			Destroy(child.gameObject);
		}

		foreach (BaseSkill skill in skills){
			GameObject entry = Instantiate(SkillEntryPrefab) as GameObject;
			entry.transform.SetParent(SkillsParent);

			LearnableSkillBehaviour behaviour = entry.GetComponent<LearnableSkillBehaviour>();
			behaviour.SetSkill(skill);
		}
	}

	public void ConfirmLevelup(){
		// Check if the player has made a skil choice
		bool success = false;
		Toggle[] toggles = SkillsParent.GetComponentsInChildren<Toggle> ();
		foreach(Toggle toggle in toggles){
			if (toggle.isOn) {
				learnSkill(toggle.GetComponentInParent<LearnableSkillBehaviour>());
				success = true;
					break;
			}
		}

		// When there are no more skills to learn, let the player just exit regardless
		if (success || toggles.Length == 0){
			RealHolder.SetActive(false);
		}
	}

	private void learnSkill(LearnableSkillBehaviour skillButton){
		PlayerSkills.LearnSkill(skillButton.skill);
	}
}