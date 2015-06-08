using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
	
	private Dictionary<Transform, AI.AiAction> _actionEntries = new Dictionary<Transform, AI.AiAction>();
	
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
		AI.AiAction action = _actionEntries[actionEntry];
		
		DisplayDescriptionOf(action.Description);
	}
	
	private void DisplayDescriptionOf(string description){
		DescriptionPanel.gameObject.SetActive(true);
		DescriptionPanel.GetComponentInChildren<Text>().text = description;
	}

	private void PopulateEffects(){

	}
}

