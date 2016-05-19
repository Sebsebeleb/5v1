namespace BBG.View.CommonUI
{
    using DG.Tweening;

    using UnityEngine;

    /// <summary>
    /// Plays a tween on the gameobject when it is activated.
    /// </summary>
    public class TransitionOnActivated : MonoBehaviour
    {

        [SerializeField]
        private Ease ease;

        [SerializeField]
        private float duration;
        public void OnEnable()
        {
            this.transform.localScale = new Vector3(0, 0, 0);
            this.transform.DOScale(new Vector3(1, 1, 1), this.duration).SetEase(this.ease);
        }
    }
}