using System;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using System.Collections.Generic;

public class EffectIconAnimation : MonoBehaviour
{
	private Image icon;

	private const float duration = 1.8f;
	private float _counter;

	private float nextAnimationTime = 0; // The time the next animation should be fired
	private Queue<string> animationQueue = new Queue<string>();

	void Awake(){
		icon = GetComponent<Image>();
	}

	void Update(){
		if (animationQueue.Count == 0){
			return;
		}

		if (nextAnimationTime < Time.time){
			nextAnimationTime = Time.time + duration;
			_initAnimation(animationQueue.Dequeue());
		}
	}

	public void StartAnimation(string iconName){
		animationQueue.Enqueue(iconName);
	}

	// TODO: stop old animation or use a queue
	private void _initAnimation(string iconName){
		//If already playing, queue the new one.
		icon.sprite = getSprite (iconName);
		icon.DOFade (1f, 0.9f).
			SetEase(Ease.InCirc).
			OnComplete(
				() => icon.DOFade(0f,0.9f).SetEase (Ease.InCirc));
	}



	private static Sprite getSprite(string iconName){
		return Resources.Load<Sprite> (iconName);
	}
}