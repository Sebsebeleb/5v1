using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using BaseClasses;

public class InspectorPanelBehaviour : MonoBehaviour{
	public GameObject ActionItemPrefab;
	
	public Text ActorName;
	public Text ActorDescription;
	public Text ActorHPText;
	public Text ActorAttackText;
	public Text ActorCooldownText;
	
	public Transform ActionsHolder;
	public Transform EffectsHolder;
	// The panel that can display detailed information about actions and effects
	public Transform DescriptionPanel;
	
	// TODO: Refactor please. This should only need one interface, not two seperate lists
	private Dictionary<Transform, AI.AiAction> _actionEntries = new Dictionary<Transform, AI.AiAction>();
	private Dictionary<Transform, Effect> _effectEntries = new Dictionary<Transform, Effect>();
	
	public void Update(){
		// Check if user wants to exit this screen
		if (Input.GetButton("Cancel")){
			gameObject.SetActive(false);
		}
	}

	public void InspectActor(Actor who){
		UpdateStats(who);
	}
	
	//Updates the basic stats
	private void UpdateStats(Actor who){
		ActorName.text = who.name;
		ActorDescription.text = "Not yet implemented";
		ActorHPText.text = string.Format("{0}/{1}", who.damagable.CurrentHealth, who.damagable.MaxHealth);
		ActorAttackText.text = who.attack.Attack.ToString();
		ActorCooldownText.text = string.Format("{0}({1})", who.countdown.CurrentCountdown, who.countdown.MaxCountdown);
		
		PopulateActions(who);
		PopulateEffects(who);
	}

	private void PopulateActions(Actor who){
		// TODO: Also temp solutuion, reset the gameobjects.
		foreach(Transform child in ActionsHolder){
			Destroy(child.gameObject);
		}
		// End 
		AI brain = who.GetComponent<AI>();

		if (!brain){
			return;
		}
		
		
		foreach (AI.AiAction action in brain.GetStandardActions()){
			CreateActionEntry(action);	
		}
		
		foreach (AI.AiAction freeAction in brain.GetFreeActions()){
			CreateActionEntry(freeAction);
		}
		
	}
	
	private void CreateActionEntry(AI.AiAction action){
		GameObject item = Instantiate(ActionItemPrefab) as GameObject;
		item.transform.SetParent(ActionsHolder);

		// TODO: this is a temp solution
		item.GetComponentInChildren<Text> ().text = action.Name;
		
		// Store a reference so we can retrieve it later if we want to get the description
		_actionEntries.Add(item.transform, action);
	}
	
	public void DisplayDescriptionForAction(Transform actionEntry){
		// TODO: Refactor pls
		if (!_actionEntries.ContainsKey(actionEntry)){
			DisplayDescriptionOf(_effectEntries[actionEntry].Description.GetDescription());
		}
		else{
			AI.AiAction action = _actionEntries[actionEntry];
			DisplayDescriptionOf(action.Description());	
		}
		
	}
	
	private void DisplayDescriptionOf(string description){
		DescriptionPanel.gameObject.SetActive(true);
		DescriptionPanel.GetComponentInChildren<Text>().text = description;
	}

	private void PopulateEffects(Actor who){
		/// TODO: Temp solution
		foreach(Transform child in EffectsHolder){
			Destroy(child.gameObject);
		}
		
		foreach(Effect eff in who.GetComponent<EffectHolder>()){
			CreateEffectEntry(eff);
		}
	}
	
	private void CreateEffectEntry(Effect eff){
		GameObject item = Instantiate(ActionItemPrefab) as GameObject;
		item.transform.SetParent(EffectsHolder);
		
		item.GetComponentInChildren<Text>().text = eff.Description.Name;
		
		_effectEntries.Add(item.transform, eff);
	}
}

