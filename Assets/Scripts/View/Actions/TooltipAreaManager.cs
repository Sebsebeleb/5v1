using System.Linq;

using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class TooltipAreaManager : MonoBehaviour
{

    public Transform Child;
    public Text Title;
    public Text Tooltip;

    // Used to get a reference to the currently selected skill
    public ToggleGroup SkillButtonToggles;

    public void SetTooltip(string title, string tooltip)
    {
        Title.DOText(title, 0.17f);
        // Messing with the outline could work.
        //Title.GetComponent<Outline>().DOColor(Color.red, 0.3f).OnComplete(() => Title.GetComponent<Outline>().DOColor(Color.black, 0.3f));

        Tooltip.text = TextUtilities.ImproveText(tooltip);

        //Tooltip.DOText(TextUtilities.ImproveText(tooltip), 0.17f).OnUpdate(() => { UpdateFontSize(); });

        // Could be cool, but unfortunately doesn't work well with the colored text
        //Tooltip.DOFade(0f, 0.1f).OnComplete(() => Tooltip.DOFade(1f, 0.1f));

        UpdateFontSize();
        Tooltip.transform.localScale = new Vector3(1, 0, 1);
        Tooltip.transform.DOScale(new Vector3(1, 1, 1), 0.15f);
    }

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
            else
            {
                Title.text = "";
                Tooltip.text = "";
            }
        }
        else
        {
            Title.text = "";
            Tooltip.text = "";
        }
    }

    //TODO: Use this/find alternative
    private void UpdateFontSize()
    {
        // This is the size calculated by the UI when the "best size" checkbox is checked on text components
        Title.fontSize = (int)(Tooltip.cachedTextGeneratorForLayout.fontSizeUsedForBestFit * 1.2f);
    }
}