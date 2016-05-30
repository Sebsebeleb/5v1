namespace BBG.View.CommonUI
{
    using System;

    using UnityEngine;
    using UnityEngine.Assertions;
    using UnityEngine.UI;

    /// <summary>
    /// uses string formating to interpret a slider's value into a text property
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class InterpretSliderAsString : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;

        [SerializeField]
        private string stringFormatting;

        [SerializeField]
        private float ValueFactor = 1f;

        private Text text;

        private float oldValue = Single.NaN;


        private void Awake()
        {
            Assert.IsNotNull(this.stringFormatting, "Missing string formatting!");
            Assert.IsNotNull(this.slider, "Missing slider component reference!");

            this.text = this.GetComponent<Text>();
        }

        void Update()
        {
            if (this.slider.value != this.oldValue)
            {
                this.oldValue = this.slider.value;

                this.text.text = this.Interpret(this.slider.value);
            }
        }

        private string Interpret(float value)
        {
            try
            {
                return string.Format(this.stringFormatting, value * this.ValueFactor);
            }
            catch (System.FormatException)
            {
                return "ERROR, please report :)";
            }
        }
    }
}