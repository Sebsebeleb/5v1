using UnityEngine;
using System.Collections;

public class ActionEntryBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ShowDescription(){
		GameObject.FindWithTag("InspectorPanel").GetComponent<InspectorPanelBehaviour>().DisplayDescriptionForAction(transform);
	}
}
