﻿using System.Collections;

using DG.Tweening;

using UnityEngine;

namespace BBG
{
    public class LightFlicker : MonoBehaviour
    {
        private Light light;

        private float rangeTarget;

        [SerializeField]
        private float minIntensity, maxIntensity;


        [SerializeField]
        private float minVariance, maxVariance;


        // Use this for initialization
        void Start ()
        {
            this.light = this.GetComponent<Light>();

            this.StartCoroutine(this.changeRangeTarget(0));

        }
    
        // Update is called once per frame
        void Update ()
        {
        }

        void OnDestroy()
        {
            DOTween.Kill(this.light);
        }

        IEnumerator changeRangeTarget(float delay)
        {
            yield return new WaitForSeconds(delay);

            float current = this.light.intensity;
            float newIntensity = Random.Range(current - this.maxVariance / 2, current + this.maxVariance / 2);

            newIntensity = Mathf.Max(newIntensity, this.minIntensity);
            newIntensity = Mathf.Min(newIntensity, this.maxIntensity);

            float del = Random.Range(0.2f, 0.8f);

            this.light.DOIntensity(newIntensity, del);

            this.StartCoroutine(this.changeRangeTarget(del));

        } 

    }
}
