using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LearnableSkillBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public BaseSkill skill;
	public Image Icon;
	public Text TooltipTitle;
	public Text TooltipDescription;

	private TooltipDisplayerBehaviour _tooltip;

	public void Awake(){
		_tooltip = GameObject.FindWithTag("TooltipDisplayer").GetComponent<TooltipDisplayerBehaviour>();
	}

	public void OnPointerEnter(PointerEventData data){
		/*RectTransform trans = GetComponent<RectTransform> ();

		Vector2 bottom = GetComponentInChildren<RectTransform> ().anchoredPosition;
		Vector2 pos = new Vector2 (bottom.x, bottom.y);
		_tooltip.SetTooltip (pos, skill.GetName (), skill.GetTooltip ());*/
	}

	public void OnPointerExit(PointerEventData data){

	}

	public void SetSkill(BaseSkill sskill){

		skill = sskill;
		// TODO: ResourceManager
        Sprite icon = Resources.Load<Sprite>(skill.GetName());
        if (icon == null) {
            Debug.LogError("Warning, couldnt load icon for skill with name: " + skill.GetName());
        }
        else {
            Icon.sprite = icon;
        }

		TooltipDescription.text = skill.GetTooltip();
		TooltipTitle.text = string.Format("{0} ({1})", skill.GetName(), skill.Rank);

		GetComponentInChildren<Toggle> ().group = GetComponentInParent<ToggleGroup> ();
	}

}