using UnityEngine;
using UnityEngine.UI;

public class TooltipDisplayerBehaviour : MonoBehaviour
{

	public Transform Child;
	public Text Title;
	public Text Tooltip;


	public ToggleGroup SkillsGroup;

	private TooltipAreaManager tooltipArea; // We tell this to update itself with the given skill
	private SkillUseButton skillBehaviour;


	void Awake(){
		skillBehaviour = GetComponent<SkillUseButton>();
		tooltipArea = GameObject.FindGameObjectWithTag("TooltipArea").GetComponent<TooltipAreaManager>();
	}

	public void SetTooltip(Vector2 position, string title, string tooltip){
		GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x, position.y + 30);
		Child.gameObject.SetActive(true);
		Title.text = title;
		Tooltip.text = tooltip;

	}

	public void DisplayMe(){
		if (skillBehaviour.AssociatedSkill == null){
			return;
		}
		tooltipArea.HoverEnter(skillBehaviour.AssociatedSkill);
	}
	public void HideMe(){
		tooltipArea.HoverExit();
	}

	//TODO: Use this/find alternative
	private void UpdateFontSize(){
		Title.fontSize = Tooltip.fontSize + 6;
	}
}