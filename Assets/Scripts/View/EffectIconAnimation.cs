using System;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;

public class EffectIconAnimation : MonoBehaviour
{
	private Image icon;

	private const float duration = 1.8f;
	private float _counter;

	void Awake(){
		icon = GetComponent<Image>();
	}

	void Start(){
	}

	// TODO: stop old animation or use a queue
	public void StartAnimation(string iconName){
		icon.sprite = getSprite (iconName);
		icon.DOFade (1f, 0.9f).SetEase(Ease.InCirc).OnComplete(() => icon.DOFade(0f,0.9f).SetEase (Ease.InCirc));
	}

	private static Sprite getSprite(string iconName){
		return Resources.Load<Sprite> (iconName);
	}
}