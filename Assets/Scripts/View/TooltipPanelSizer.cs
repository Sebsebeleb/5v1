using UnityEngine;

// Simple script that updates the layout element's preferred size to a percentage of the screen width
namespace BBG.View
{
    using UnityEngine.UI;

    public class TooltipPanelSizer : MonoBehaviour {
	
        public float ScreenFactor;
	
        private int _oldWidth;

        private LayoutElement element;
	
        void Awake(){
            this.element = this.GetComponent<LayoutElement>();		
        }

        void Start () {
            this.UpdateWidth();
        }
	
        void Update () {
            if (Screen.width != this._oldWidth){
                this.UpdateWidth();
            }
        }
	
        private void UpdateWidth(){
            this.element.preferredWidth = Screen.width * this.ScreenFactor - 5;
            this._oldWidth = Screen.width;
        }
    }
}
