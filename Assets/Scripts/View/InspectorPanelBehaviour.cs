using UnityEngine;
using UnityEngine.UI;

public class InspectorPanelBehaviour : MonoBehaviour{
	public GameObject ActionItemPrefab;
	
	public Text ActorName;
	public Text ActorDescription;
	public Text ActorHPText;
	public Text ActorAttackText;
	public Text ActorCooldownText;
	
	public Transform ActionsHolder;
	public Transform EffectsHolder;
	
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
	}

	private void PopulateActions(){

	}

	private void PopulateEffects(){

	}
}

