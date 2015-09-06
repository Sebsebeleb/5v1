using System;
using UnityEngine;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using System.Collections.Generic;

public class EffectIconAnimation : MonoBehaviour
{
	private Image icon;

	private float _counter;

	private float nextAnimationTime = 0; // The time the next animation should be fired
	private Queue<string> animationQueue = new Queue<string>();

	private List<Tweener> currentTweens = new List<Tweener>();

	void Awake(){
		icon = GetComponent<Image>();
	}

	void Update(){
		float totalDuration = 0;
		totalDuration += animationQueue.Count * Settings.IconAnimationTime;

		// The duration left of the currently playing icon animation
		float remainingCurrentTween = 0;

		if (currentTweens.Count > 0){
			remainingCurrentTween = currentTweens[0].Duration(false) - currentTweens[0].Elapsed(false);
			totalDuration += remainingCurrentTween;
		}

		// The duration that each playing and queued animation will have
		float newDurationPerIcon = totalDuration / (currentTweens.Count + animationQueue.Count);

		float delay = 0f;
		// Start the new animations
		while (animationQueue.Count > 0){
			string icon = animationQueue.Dequeue();
			Tweener tween = _initAnimation(icon, newDurationPerIcon, delay);
			delay += newDurationPerIcon;
			currentTweens.Add(tween);
		}

	}

	public void StartAnimation(string iconName){
		animationQueue.Enqueue(iconName);
	}

	// TODO: stop old animation or use a queue
	private Tweener _initAnimation(string iconName, float duration, float delay){
		//If already playing, queue the new one.

		Tweener tween = icon.DOFade (1f, duration/2)
			.SetEase(Ease.InCirc)
			.SetDelay(delay)
			.OnStart(() => icon.sprite = getSprite (iconName));

		tween.OnComplete ( ()=> {
			icon.DOFade(0f,duration/2).SetEase (Ease.InCirc);
			currentTweens.Remove(tween);
		});

		return tween;
	}



	private static Sprite getSprite(string iconName){
		return Resources.Load<Sprite> (iconName);
	}
}