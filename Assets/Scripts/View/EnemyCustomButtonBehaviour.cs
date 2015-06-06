using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyCustomButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
	public GameObject InspectorPanel;
	private InspectorPanelBehaviour _inspector;
	
	private View.GridButtonBehaviour _gridButton;
	
	// Used to tell the player we want to target us
	private PlayerTargeting _playerTargeting;
	
	void Awake(){
		_gridButton = GetComponent<View.GridButtonBehaviour>();
		_inspector = InspectorPanel.GetComponent<InspectorPanelBehaviour>();
		_playerTargeting = GameObject.FindWithTag("Player").GetComponent<PlayerTargeting>();
	}
	
	public void OnPointerClick(PointerEventData eventData){
		// Rightclick -> Inspect		
		if (eventData.button == PointerEventData.InputButton.Right){
			InspectEnemy();
		}
		
		// Otherwise, target it
		else{
			TargetEnemy();
		}
	}

	private void InspectEnemy(){
		Actor enemy = GridManager.TileMap.GetAt(_gridButton.x, _gridButton.y);
		InspectorPanel.SetActive (true);
		
		_inspector.InspectActor (enemy);
	}
	private void TargetEnemy(){
		_playerTargeting.TargetGrid(_gridButton.x, _gridButton.y);
	}
}
