using UnityEngine;

namespace BBG.View.CommonUI
{
    using UnityEngine.UI;

    public class SegmentedBar : MonoBehaviour, IMaterialModifier
    {

        private float fill = 1f;


        private float segmentSpacing = 1;

        private int numSegments = 1;

        private Material mat;

        public float Fill
        {
            set
            {
                this.fill = value;
                if (this.mat == null)
                {
                    return;
                }
                this.mat.SetFloat("_Fill", value);
            }

            get
            {
                return this.fill;
            }
        }

        public int NumSegments
        {
            set
            {
                this.numSegments = value;
                if (this.mat == null)
                {
                    return;
                }
                this.mat.SetFloat("_NumSegments", value);
            }

            get
            {
                return this.numSegments;
            }
        }

        public float SegmentSpacing
        {
            set
            {
                this.segmentSpacing = value;
                if (this.mat == null)
                {
                    return;
                }
                this.mat.SetFloat("_SegmentSpacing", value);
            }

            get
            {
                return this.segmentSpacing;
            }
        }

        public void Awake()
        {
        

        }


        public Material GetModifiedMaterial(Material baseMaterial)
        {
            this.mat = new Material(baseMaterial);


            this.mat.SetFloat("_Fill", this.fill);
            this.mat.SetFloat("_NumSegments", this.numSegments);
            this.mat.SetFloat("_SegmentSpacing", this.segmentSpacing);

            return this.mat;
        }
    }
}
