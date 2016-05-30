namespace BBG.View.Juiciness
{
    using DG.Tweening;

    using UnityEngine;
    public class SliderHandleJuice : MonoBehaviour
    {
        public Transform handle;

        public void BeginJuice()
        {
            handle.DOKill();
            handle.DOScale(new Vector3(0.5f, 0.95f), 0.5f).SetEase(Ease.OutExpo);
        }

        public void EndJuice()
        {
            handle.DOKill();
            handle.DOScale(new Vector3(1f, 1f), 0.3f);
        }
    }
}