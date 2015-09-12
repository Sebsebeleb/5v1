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
    private Queue<ChangeAnimation> animationQueue = new Queue<ChangeAnimation>();

    private List<Tweener> currentTweens = new List<Tweener>();

    public GameObject FloatTextPrefab;

    void Awake()
    {
        icon = GetComponent<Image>();
    }

    void Update()
    {
        float totalDuration = 0;
        totalDuration += animationQueue.Count * Settings.IconAnimationTime;

        // The duration left of the currently playing icon animation
        float remainingCurrentTween = 0;

        if (currentTweens.Count > 0)
        {
            remainingCurrentTween = currentTweens[0].Duration(false) - currentTweens[0].Elapsed(false);
            totalDuration += remainingCurrentTween;
        }

        // The duration that each playing and queued animation will have
        float newDurationPerIcon = totalDuration / (currentTweens.Count + animationQueue.Count);

        float delay = 0f;
        // Start the new animations
        while (animationQueue.Count > 0)
        {
            ChangeAnimation animInfo = animationQueue.Dequeue();
            Tweener tween = _initAnimation(animInfo, newDurationPerIcon, delay);
            delay += newDurationPerIcon;
            currentTweens.Add(tween);
        }

    }

    public void StartAnimation(ChangeAnimation animationInfo, string floatingText)
    {

        animationQueue.Enqueue(animationInfo);
    }

    // TODO: stop old animation or use a queue
    private Tweener _initAnimation(ChangeAnimation animationInfo, float duration, float delay)
    {
        //If already playing, queue the new one.

        Tweener tween;

        if (animationInfo.SpawnHoverText)
        {
            tween = icon.DOFade(1f, duration / 2)
            .SetEase(Ease.InCirc)
            .SetDelay(delay)
            .OnStart(() =>
                {
                    icon.sprite = getSprite(animationInfo.IconName);
                    // Text prop stuff
                    GameObject text = Instantiate(FloatTextPrefab) as GameObject;
                    text.transform.SetParent(GameObject.FindWithTag("MainCanvas").transform);
                    text.GetComponent<FloatTextBehaviour>().SetText(animationInfo.TextProp);
                    text.GetComponent<FloatTextBehaviour>().SetTarget(animationInfo.target.GetComponent<RectTransform>());
                });

            tween.OnComplete(() =>
                {
                    icon.DOFade(0f, duration / 2).SetEase(Ease.InCirc);

                    currentTweens.Remove(tween);
                });
        }
        else
        {
            tween = icon.DOFade(1f, duration / 2)
            .SetEase(Ease.InCirc)
            .SetDelay(delay)
            .OnStart(() =>
                {
                    icon.sprite = getSprite(animationInfo.IconName);
                });

            tween.OnComplete(() =>
            {
                icon.DOFade(0f, duration / 2).SetEase(Ease.InCirc);
                currentTweens.Remove(tween);
            });
        }

        return tween;
    }



    private static Sprite getSprite(string iconName)
    {
        return Resources.Load<Sprite>(iconName);
    }
}