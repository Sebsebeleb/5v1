using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class LearnableSkillBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public BaseSkill skill;
	public Image Icon;
    public Toggle toggle;

    public bool IsOldSkill;

    private LevelupManagerBehaviour _manager;

	public void Awake(){
        _manager = GameObject.FindWithTag("LevelupManager").GetComponent<LevelupManagerBehaviour>();
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

		string prettified = TextUtilities.ImproveText(skill.GetTooltip());
		//TooltipDescription.text = prettified;
		//TooltipTitle.text = string.Format("{0} ({1})", skill.GetName(), skill.Rank);

	}

    /// <summary>
    /// Called by the ui system when the toggle for this button has changed. Propagates this information to the levelup manager
    /// </summary>
    /// <param name="v">if toggled on or off</param>
    public void ToggleChanged(bool v)
    {
        if (!v) return;

        if (IsOldSkill)
        {
            if (skill == null)
            {
                _manager.SetSelectedOldSkill();
            }
            else
            {
                _manager.SetSelectedOldSkill(skill);
            }
        }
        else
        {
            _manager.SetSelectedNewSkill(skill);
        }
    }
}