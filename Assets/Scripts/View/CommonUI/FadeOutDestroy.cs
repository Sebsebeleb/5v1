namespace BBG.View.CommonUI
{
    using DG.Tweening;

    using UnityEngine;
    /// <summary>
    /// Fades an object out before destroying it. TODO: Should probably make a single monobehaviour for multiple customizable behaviours like this instead of having a clutter of different similar ones
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeOutDestroy : MonoBehaviour
    {

        [Range(0, 10)]
        public float Delay;

        [Range(0, 10)]
        public float Duration;

        void Start()
        {
            GetComponent<CanvasGroup>()
                .DOFade(0f, this.Duration)
                .SetDelay(this.Delay)
                .OnComplete(() => { Destroy(gameObject); });
                                                                                                            ;
            }
    }
}