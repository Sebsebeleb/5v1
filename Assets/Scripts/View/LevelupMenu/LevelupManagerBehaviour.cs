using UnityEngine;

public class LevelupManagerBehaviour : MonoBehaviour
{
	public Transform SkillsParent;
	public GameObject SkillEntryPrefab;
	
	void Awake(){
		//delegateeventCallback = OnPlayerLevel;
		Event.PlayerLeveledUp callback = OnPlayerLevel;
		Event.EventManager.Register(Event.Events.PlayerLeveledUp, callback);
	}
	
	void Start(){
		gameObject.SetActive(false);
	}
	
	// notused parameter is a lazy as fuck way of dealing with the event manager.
	private void OnPlayerLevel(int i){
		BaseSkill[] skills = new BaseSkill[3]{
			new Data.Skills.Block(),
			new Data.Skills.Bloodlust(),
			new Data.Skills.Cleave()
		};
		
		Init(skills);
	}
	
	public void Init(BaseSkill[] skills){
		gameObject.SetActive(true);
		foreach (BaseSkill skill in skills){
			GameObject entry = Instantiate(SkillEntryPrefab) as GameObject;
			entry.transform.SetParent(SkillsParent);
			
			LearnableSkillBehaviour behaviour = entry.GetComponent<LearnableSkillBehaviour>();
			behaviour.SetSkill(skill);
		}	
	}	
	
	public void ConfirmLevelup(){
		
	}
}