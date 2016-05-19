namespace BBG.View
{
    using UnityEngine;

    public class ForceScale : MonoBehaviour
    {
        public void Update()
        {
            Vector3 newVec = this.transform.localScale;
            newVec.x = 1.0f;

            this.transform.localScale = newVec;
        }
    }
}
