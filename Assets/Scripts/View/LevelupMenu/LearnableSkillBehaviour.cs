using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class LearnableSkillBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private BaseSkill _skill;
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
		_tooltip.SetTooltip (pos, _skill.GetName (), _skill.GetTooltip ());*/
	}
	
	public void OnPointerExit(PointerEventData data){
		
	}
	
	public void SetSkill(BaseSkill skill){
		
		_skill = skill;
		// TODO: ResourceManager
        Sprite icon = Resources.Load<Sprite>(_skill.GetName());
        if (icon == null) {
            Debug.LogError("Warning, couldnt load icon for skill with name: " + _skill.GetName());
        }
        else {
            Icon.sprite = icon;
        }
		
		TooltipDescription.text = skill.GetTooltip();
		TooltipTitle.text = skill.GetName();
		
		GetComponentInChildren<Toggle> ().group = GetComponentInParent<ToggleGroup> ();
	}
	
}