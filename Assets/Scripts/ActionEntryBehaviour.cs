using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionEntryBehaviour : MonoBehaviour {

	public Text DurationText;
	public Text TitleText;
	public Image Icon;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
	public void ShowDescription(){
		GameObject.FindWithTag("InspectorPanel").GetComponent<InspectorPanelBehaviour>().DisplayDescriptionForEntry(transform);
	}
}
