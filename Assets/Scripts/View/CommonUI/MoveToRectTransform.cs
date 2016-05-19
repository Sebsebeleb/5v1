namespace BBG.View.CommonUI
{

    using UnityEngine;
    using UnityEngine.UI.Extensions;

    /// <summary>
    /// On enable, this monobehaviour will move to the target + an offset
    /// </summary>
    public class MoveToRectTransform : MonoBehaviour
    {
        [SerializeField]
        private RectTransform target;

        [SerializeField]
        private Vector2 offset;


        public void OnEnable()
        {
            if (this.target != null)
            {
                this.Move(this.target, this.offset);
            }
        }

        public void Move(RectTransform to, Vector2 offset)
        {
            if (offset == null)
            {
                offset = Vector2.zero;
            }

            RectTransform trans = this.GetComponent<RectTransform>();

            trans.anchoredPosition = to.switchToRectTransform(trans) + offset;
        }

    }
}