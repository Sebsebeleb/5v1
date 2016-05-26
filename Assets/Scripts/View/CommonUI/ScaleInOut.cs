namespace TweenAnimations
{
    using DG.Tweening;

    using UnityEngine;

    [ExecuteInEditMode]
    public class ScaleInOut : MonoBehaviour
    {
        public Ease EaseIn, EaseOut;

        public float max, min, duration;

        [Tooltip("If relative, max and min will be relative to current scale")]
        public bool relativeScale = true;

        void Start()
        {
            Vector3 maxScale;
            Vector3 minScale;
            if (this.relativeScale)
            {
                maxScale = transform.localScale * this.max;
                minScale = transform.localScale * this.min;
            }
            else
            {
                maxScale = Vector3.one * this.max;
                minScale = Vector3.one * this.min;
            }

            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOBlendableScaleBy(minScale, this.duration / 2).SetEase(EaseIn));
            sequence.Append(transform.DOBlendableScaleBy(maxScale, this.duration / 2).SetEase(EaseOut));

            sequence.SetLoops(-1, LoopType.Restart);
        }

        public void OnDisable()
        {
            transform.DOKill(true);

        }
    }
}