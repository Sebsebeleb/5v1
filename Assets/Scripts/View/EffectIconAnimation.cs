using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
// Asset package

namespace BBG.View
{
    using BBG.Settings;

    using UnityEngine.UI;
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
            this.icon = this.GetComponent<Image>();
        }

        void Update()
        {
            float totalDuration = 0;
            totalDuration += this.animationQueue.Count * Settings.IconAnimationTime;

            // The duration left of the currently playing icon animation
            float remainingCurrentTween = 0;

            if (this.currentTweens.Count > 0)
            {
                remainingCurrentTween = this.currentTweens[0].Duration(false) - this.currentTweens[0].Elapsed(false);
                totalDuration += remainingCurrentTween;
            }

            // The duration that each playing and queued animation will have
            float newDurationPerIcon = totalDuration / (this.currentTweens.Count + this.animationQueue.Count);

            float delay = 0f;
            // Start the new animations
            while (this.animationQueue.Count > 0)
            {
                ChangeAnimation animInfo = this.animationQueue.Dequeue();
                Tweener tween = this._initAnimation(animInfo, newDurationPerIcon, delay);
                delay += newDurationPerIcon;
                this.currentTweens.Add(tween);
            }

        }

        public void StartAnimation(ChangeAnimation animationInfo, string floatingText)
        {
            this.animationQueue.Enqueue(animationInfo);
        }

        // TODO: stop old animation or use a queue
        private Tweener _initAnimation(ChangeAnimation animationInfo, float duration, float delay)
        {
            //If already playing, queue the new one.

            Tweener tween;

            if (animationInfo.SpawnHoverText)
            {
                var outline = this.icon.GetComponent<NicerOutline>();

                float fade;

                if (string.IsNullOrEmpty(animationInfo.IconName))
                {
                    fade = 0f;
                }
                else
                {
                    fade = 1f;
                }
                tween = this.icon.DOFade(fade, duration / 2)
                    .SetEase(Ease.InCirc)
                    .SetDelay(delay)
                    .OnStart(() =>
                        {

                            if (!string.IsNullOrEmpty(animationInfo.IconName))
                            {
                                this.icon.sprite = getSprite(animationInfo.IconName);
                            }
                            else
                            {
                                this.icon.sprite = null;
                            }
                            // Text prop stuff
                            GameObject text = Instantiate(this.FloatTextPrefab) as GameObject;
                            text.transform.SetParent(GameObject.FindWithTag("MainCanvas").transform);
                            text.GetComponent<FloatTextBehaviour>().SetText(animationInfo.TextProp);
                            text.GetComponent<FloatTextBehaviour>().SetTarget(animationInfo.target.GetComponent<RectTransform>());
                            if (animationInfo.FontSize != 0)
                            {
                                text.GetComponent<Text>().fontSize = animationInfo.FontSize;
                            }
                        });
                // Tween the outline as well, otherwise it looks pretty dark and weird in the start
                tween.OnUpdate(() =>
                {
                    Color old = outline.effectColor;
                    old.a = this.icon.color.a * 0.6f;
                    outline.effectColor = old;
                });
                tween.OnComplete(() =>
                    {
                        this.icon.DOFade(0f, duration / 2).SetEase(Ease.InCirc);

                        this.currentTweens.Remove(tween);
                    });

            }
            else
            {
                tween = this.icon.DOFade(1f, duration / 2)
                    .SetEase(Ease.InCirc)
                    .SetDelay(delay)
                    .OnStart(() =>
                        {
                            this.icon.sprite = getSprite(animationInfo.IconName);
                        });

                tween.OnComplete(() =>
                    {
                        this.icon.DOFade(0f, duration / 2).SetEase(Ease.InCirc);
                        this.currentTweens.Remove(tween);
                    });
            }

            return tween;
        }



        private static Sprite getSprite(string iconName)
        {
            return Resources.Load<Sprite>(iconName);
        }
    }
}