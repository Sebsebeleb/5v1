using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;


///
/// The class responsible for updating the text of the description area on the main screen
///
/// TODO: Make the scale tweens queue after each other rather than overwriting eachother
/// TODO: zzz bugged it up, fix later

public class TooltipAreaManager : MonoBehaviour
{

    public Transform Child;
    public Text Title;
    public Text Tooltip;

    // The skill that is currently being described
    private string currentlyDisplaying = "";
    private bool currentlyHovering = false;

    // Current tween
    private Sequence seq;

    // Used to get a reference to the currently selected skill
    public ToggleGroup SkillButtonToggles;

    private void Start()
    {
        seq = DOTween.Sequence();
        seq.SetAutoKill(false);
    }

    private void Update(){
        // Stop displaying the description for a tooltip if it is no longer selected
        if (!currentlyHovering && currentlyDisplaying != "" && !SkillButtonToggles.AnyTogglesOn()){
            Clear();
        }
    }

    public void SetTooltip(string title, string tooltip)
    {
        // If the title is the same, we assume it's the same skill. and dont redraw
        if (title == currentlyDisplaying)
        {
            Debug.Log("Bye");
            return;
        }

        currentlyDisplaying = title;

        Title.DOText(title, 0.17f);
        // Messing with the outline could work.
        //Title.GetComponent<Outline>().DOColor(Color.red, 0.3f).OnComplete(() => Title.GetComponent<Outline>().DOColor(Color.black, 0.3f));

        Tooltip.text = TextUtilities.ImproveText(tooltip);

        //Tooltip.DOText(TextUtilities.ImproveText(tooltip), 0.17f).OnUpdate(() => { UpdateFontSize(); });

        // Could be cool, but unfortunately doesn't work well with the colored text
        //Tooltip.DOFade(0f, 0.1f).OnComplete(() => Tooltip.DOFade(1f, 0.1f));

        UpdateFontSize();
        Tooltip.transform.localScale = new Vector3(1, 0, 1);
        seq.Append(Tooltip.transform.DOScale(new Vector3(1, 1, 1), 0.15f));
    }

    public void HoverEnter(string title, string tooltip){
        currentlyHovering = true;

        SetTooltip(title, tooltip);
    }

    public void HoverExit(){
        currentlyHovering = false;

        Clear();
    }

    //Called by unity when the pointer leaves the skill area
    public void Clear()
    {
        //Check if we have a skill selected
        //If we do, we use that skill's description rather than clearing it
        if (SkillButtonToggles.AnyTogglesOn())
        {
            Toggle active = SkillButtonToggles.ActiveToggles().ToList()[0];
            BaseSkill skill = active.GetComponent<SkillUseButton>().AssociatedSkill;

            if (skill != null)
            {
                SetTooltip(skill.GetName(), skill.GetTooltip());
            }
        }

        currentlyDisplaying = "";

        Title.DOText("", 0.17f);
        //Tooltip.transform.localScale = new Vector3(1, 1, 1);
        seq.Append(Tooltip.transform.DOScale(new Vector3(1, 0, 1), 0.125f));
        //Tooltip.DOText("", 0.17f);
    }

    //TODO: Use this/find alternative
    private void UpdateFontSize()
    {
        // This is the size calculated by the UI when the "best size" checkbox is checked on text components
        Title.fontSize = (int)(Tooltip.cachedTextGeneratorForLayout.fontSizeUsedForBestFit * 1.2f);
    }
}