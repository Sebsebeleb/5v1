namespace BBG.Tutorial
{
    using UnityEngine;

    public class Highlighter : MonoBehaviour
    {
        [SerializeField]
        private GameObject highlightFrame;

        private GameObject objRef;

        [SerializeField]
        private RectTransform target;

        void Start()
        {
            if (this.target != null)
            {
                this.Highlight(this.target);
            }
        }

        void OnDisable()
        {
            if (this.objRef != null)
            {
                Destroy(this.objRef);
            }
        }

        /// <summary>
        /// Display the highlight frame around the target rect transform
        /// </summary>
        /// <param name="tgt"></param>
        public void Highlight(RectTransform tgt)
        {
            this.objRef = Instantiate(this.highlightFrame);

            this.objRef.transform.SetParent(tgt, false);
        }
    }
}