using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Simple script that updates the layout element's preferred size to a percentage of the screen width
public class TooltipPanelSizer : MonoBehaviour {
	
	public float ScreenFactor;
	
	private int _oldWidth;

	private LayoutElement element;
	
	void Awake(){
		element = GetComponent<LayoutElement>();		
	}

	void Start () {
		UpdateWidth();
	}
	
	void Update () {
		if (Screen.width != _oldWidth){
			UpdateWidth();
		}
	}
	
	private void UpdateWidth(){
		element.preferredWidth = Screen.width * ScreenFactor - 5;
		_oldWidth = Screen.width;
	}
}
