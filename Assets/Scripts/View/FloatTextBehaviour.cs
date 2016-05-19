using DG.Tweening;

using UnityEngine;

namespace BBG.View
{
    using UnityEngine.UI;

    public class FloatTextBehaviour : MonoBehaviour
    {
        public Text text;

        private const float duration = 1.8f; // Should maybe be a setting (for example use AnimationManager.AniamtionDuration instead)
        private float _counter;

        void Awake(){
            this.text = this.GetComponent<Text>();
        }

        void Start(){
            this._counter = 0f;
            this.transform.DOMoveY (this.transform.position.y+50, 1.8f);
            this.GetComponent<Text> ().DOFade (1f, 0.4f);
        }

        void Update(){
            this._counter += Time.deltaTime;

            if (this._counter >= duration){
                Destroy(this.gameObject);
            }
        }

        public void SetText(string _text){
            this.text.text = _text;
        }

        public void SetTarget(RectTransform target){
            this.transform.position = target.transform.position;
        }


    }
}