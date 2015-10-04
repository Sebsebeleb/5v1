using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class TooltipAreaManager : MonoBehaviour
{

	public Transform Child;
	public Text Title;
	public Text Tooltip;

	void Awake(){

	}

	public void SetTooltip(string title, string tooltip){
		//Child.gameObject.SetActive(true);
		//Title.text = title;
		//Tooltip.text = tooltip;
		Title.DOText(title, 0.2f);
		Tooltip.DOText(tooltip, 0.2f).OnUpdate(() => {UpdateFontSize();});



	}

	public void Clear(){
		//Child.gameObject.SetActive(false);
		Title.text = "";
		Tooltip.text = "";
	}

	//TODO: Use this/find alternative
	private void UpdateFontSize(){
		// This is the size calculated by the UI when the "best size" checkbox is checked on text components
		Title.fontSize = (int) (Tooltip.cachedTextGeneratorForLayout.fontSizeUsedForBestFit * 1.1f);
		//Tooltip.
		Debug.Log(Title.fontSize);
	}
}