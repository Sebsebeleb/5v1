using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FloatTextBehaviour : MonoBehaviour
{
	public Text text;

	private const float duration = 1.8f; // Should maybe be a setting (for example use AnimationManager.AniamtionDuration instead)
	private float _counter;

	void Awake(){
		text = GetComponent<Text>();
	}

	void Start(){
		_counter = 0f;
		transform.DOMoveY (transform.position.y+50, 1.8f);
		GetComponent<Text> ().DOFade (1f, 0.4f);
	}

	void Update(){
		_counter += Time.deltaTime;

		if (_counter >= duration){
			Destroy(gameObject);
		}
	}

	public void SetText(string _text){
		text.text = _text;
	}

	public void SetTarget(RectTransform target){
		transform.position = target.transform.position;
	}


}