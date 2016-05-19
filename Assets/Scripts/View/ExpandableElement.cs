using DG.Tweening;

using UnityEngine;

namespace BBG.View
{
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    using Debug = UnityEngine.Debug;

    [RequireComponent(typeof (LayoutElement))]
    public class ExpandableElement : MonoBehaviour, IPointerClickHandler
    {

        private bool _isCollapsed = false;
        private bool _needsUpdate = false;

        // The height to use for layout element when the gameobject is expanded
        public float CollapsedHeight;
        private LayoutElement _element;
        public ILayoutElement SizeController; // The component to use for getting the real wanted size when expanded

        // If this is true, the box will be collapsed by default when it is enabled from being deactivated
        public bool CollapsedByDefault;

        //Will clicking the gameobject this is on toggle it?
        public bool ToggleOnClick;


        private void Awake()
        {
            this._element = this.GetComponent<LayoutElement>();
        }

        // Ensure the size is correct (could have been changed, like child text changing meaning the expanded size is wrong
        public void OnEnable()
        {
            if (this.CollapsedByDefault)
            {
                this._isCollapsed = true;
            }
            // Preferred size isnt updated untill the next frame so we need to wait a bit
            this._needsUpdate = true;
        }

        /// <summary>
        /// Instantaneously updates to correct size based on state.
        /// </summary>
        private void UpdateSizes()
        {
            if (this.IsCollapsed)
            {
                this._element.preferredHeight = this.CollapsedHeight;
            }
            else
            {
                this._element.preferredHeight = -1f;
                Debug.Log(LayoutUtility.GetPreferredHeight(this.GetComponent<RectTransform>()));
                this._element.preferredHeight = LayoutUtility.GetPreferredHeight(this.GetComponent<RectTransform>());
            }
        }

        public void LateUpdate()
        {
            if (this._needsUpdate)
            {
                this._needsUpdate = false;

                this.UpdateSizes();
            }
        }

        public bool IsCollapsed
        {
            get { return this._isCollapsed; }
            set { this._isCollapsed = value; }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (this.ToggleOnClick)
            {
                this.ToggleExpanded();
            }
        }

        private void UpdateState()
        {

            if (this.IsCollapsed)
            {
                this._element.DOPreferredSize(new Vector2(-1, this.CollapsedHeight), 0.3f);
            }
            else
            {
                float oldSize = this._element.preferredHeight;

                // Silly way to get the actual preferred (ignoring our own layout element) height.
                this._element.preferredHeight = -1f;
                float fullSize = LayoutUtility.GetPreferredHeight(this.GetComponent<RectTransform>());
                this._element.preferredHeight = oldSize;
                this._element.DOPreferredSize(new Vector2(-1, fullSize), 0.3f);
            }
        
        }

        /// <summary>
        /// Expands if collapsed, or collapses if expanded
        /// </summary>
        public void ToggleExpanded()
        {
            this.IsCollapsed = !this.IsCollapsed;

            this.UpdateState();
        }

        public void ToggleExpanded(bool expand)
        {
            if (expand != this.IsCollapsed)
            {
                return;
            }

            this.IsCollapsed = !expand;

            this.UpdateState();
        }
    }
}