using System.Linq;

using DG.Tweening;

using UnityEngine;

namespace BBG.View.Actions
{
    using BBG.BaseClasses;

    using UnityEngine.UI;

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

        public Text ManaText;
        public Text CooldownText;

        // The skill that is currently being described
        private string currentlyDisplaying = "";
        private bool currentlyHovering = false;

        // Current tween
        private Sequence seq;

        // Used to get a reference to the currently selected skill
        public ToggleGroup SkillButtonToggles;

        private void Start()
        {
            this.seq = DOTween.Sequence();
            this.seq.SetAutoKill(false);
        }

        private void Update(){
            // Stop displaying the description for a tooltip if it is no longer selected
            if (!this.currentlyHovering && this.currentlyDisplaying != "" && !this.SkillButtonToggles.AnyTogglesOn()){
                this.Clear();
            }
        }

        public void SetTooltip(BaseSkill skill)
        {
            string title = skill.GetName();
            string tooltip = skill.GetTooltip();
            // If the title is the same, we assume it's the same skill. and dont redraw
            if (title == this.currentlyDisplaying)
            {
                return;
            }

            this.currentlyDisplaying = title;

            this.Title.DOText(title, 0.17f);
            // Messing with the outline could work.
            //Title.GetComponent<Outline>().DOColor(Color.red, 0.3f).OnComplete(() => Title.GetComponent<Outline>().DOColor(Color.black, 0.3f));

            this.Tooltip.text = TextUtilities.ImproveText(tooltip);

            this.ManaText.text = skill.ManaCost.ToString();
            this.CooldownText.text = skill.BaseCooldown.ToString();

            //Tooltip.DOText(TextUtilities.ImproveText(tooltip), 0.17f).OnUpdate(() => { UpdateFontSize(); });

            // Could be cool, but unfortunately doesn't work well with the colored text
            //Tooltip.DOFade(0f, 0.1f).OnComplete(() => Tooltip.DOFade(1f, 0.1f));

            //UpdateFontSize();
            this.Tooltip.transform.localScale = new Vector3(1, 0, 1);
            this.seq.Append(this.Tooltip.transform.DOScale(new Vector3(1, 1, 1), 0.15f));
            this.seq.Append(this.ManaText.transform.parent.DOScale(new Vector3(1, 1, 1), 0.15f));
            this.seq.Append(this.CooldownText.transform.parent.DOScale(new Vector3(1, 1, 1), 0.15f));
        }

        public void HoverEnter(BaseSkill skill){

            this.currentlyHovering = true;

            this.SetTooltip(skill);
        }

        public void HoverExit(){
            this.currentlyHovering = false;

            this.Clear();
        }

        //Called by unity when the pointer leaves the skill area
        public void Clear()
        {
            //Check if we have a skill selected
            //If we do, we use that skill's description rather than clearing it
            if (this.SkillButtonToggles.AnyTogglesOn())
            {
                Toggle active = this.SkillButtonToggles.ActiveToggles().ToList()[0];
                BaseSkill skill = active.GetComponent<SkillUseButton>().AssociatedSkill;

                if (skill != null)
                {
                    this.SetTooltip(skill);
                }
            }

            this.currentlyDisplaying = "";

            this.Title.DOText("", 0.17f);
            //Tooltip.transform.localScale = new Vector3(1, 1, 1);
            this.seq.Append(this.Tooltip.transform.DOScale(new Vector3(1, 0, 1), 0.125f));
            this.seq.Append(this.ManaText.transform.parent.DOScale(new Vector3(0, 0.5f, 1), 0.125f));
            this.seq.Append(this.CooldownText.transform.parent.DOScale(new Vector3(0, 0.5f, 1), 0.125f));
            //Tooltip.DOText("", 0.17f);
        }

        //TODO: Use this/find alternative
        private void UpdateFontSize()
        {
            // This is the size calculated by the UI when the "best size" checkbox is checked on text components
            this.Title.fontSize = (int)(this.Tooltip.cachedTextGeneratorForLayout.fontSizeUsedForBestFit * 1.2f);
        }
    }
}