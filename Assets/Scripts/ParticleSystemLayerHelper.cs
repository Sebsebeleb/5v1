namespace BBG
{
    using UnityEngine;

    [ExecuteInEditMode]
    class ParticleSystemLayerHelper : MonoBehaviour 
    {
        void Start()
        {
            ParticleSystem ps = this.GetComponent<ParticleSystem>();
            ps.GetComponent<Renderer>().sortingLayerName = "Particles";
        }
    }
}
