using UnityEngine;

namespace BBG.View
{
    using BBG.View.Actions;

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
            this.skillBehaviour = this.GetComponent<SkillUseButton>();
            this.tooltipArea = GameObject.FindGameObjectWithTag("TooltipArea").GetComponent<TooltipAreaManager>();
        }

        public void SetTooltip(Vector2 position, string title, string tooltip){
            this.GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x, position.y + 30);
            this.Child.gameObject.SetActive(true);
            this.Title.text = title;
            this.Tooltip.text = tooltip;

        }

        public void DisplayMe(){
            if (this.skillBehaviour.AssociatedSkill == null){
                return;
            }
            this.tooltipArea.HoverEnter(this.skillBehaviour.AssociatedSkill);
        }
        public void HideMe(){
            this.tooltipArea.HoverExit();
        }

        //TODO: Use this/find alternative
        private void UpdateFontSize(){
            this.Title.fontSize = this.Tooltip.fontSize + 6;
        }
    }
}