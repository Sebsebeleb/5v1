using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyCustomButtonBehaviour : MonoBehaviour, IPointerClickHandler
{
	public GameObject InspectorPanel;

	public void OnPointerClick(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Right){
			InspectEnemy();
		}
		else{
			TargetEnemy();
		}
	}

	private void InspectEnemy(){
		InspectorPanel.SetActive (true);	
	}
	private void TargetEnemy(){

	}
}
