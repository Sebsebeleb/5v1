using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Asset package
using UnityEngine.UI.Extensions;

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
            var outline = icon.GetComponent<NicerOutline>();

            float fade;

            if (string.IsNullOrEmpty(animationInfo.IconName))
            {
                fade = 0f;
            }
            else
            {
                fade = 1f;
            }
            tween = icon.DOFade(fade, duration / 2)
            .SetEase(Ease.InCirc)
            .SetDelay(delay)
            .OnStart(() =>
                {

                    if (!string.IsNullOrEmpty(animationInfo.IconName))
                    {
                        icon.sprite = getSprite(animationInfo.IconName);
                    }
                    else
                    {
                        this.icon.sprite = null;
                    }
                    // Text prop stuff
                    GameObject text = Instantiate(FloatTextPrefab) as GameObject;
                    text.transform.SetParent(GameObject.FindWithTag("MainCanvas").transform);
                    text.GetComponent<FloatTextBehaviour>().SetText(animationInfo.TextProp);
                    text.GetComponent<FloatTextBehaviour>().SetTarget(animationInfo.target.GetComponent<RectTransform>());
                    if (animationInfo.FontSize != 0)
                    {
                        text.GetComponent<Text>().fontSize = animationInfo.FontSize;
                    }
                });
            // Tween the outline as well, otherwise it looks pretty dark and weird in the start
            tween.OnUpdate(() => {
                Color old = outline.effectColor;
                old.a = icon.color.a * 0.6f;
                outline.effectColor = old;
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