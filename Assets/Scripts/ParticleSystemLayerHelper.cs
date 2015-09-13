using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace Assets.Scripts
{
    [ExecuteInEditMode]
    class ParticleSystemLayerHelper : MonoBehaviour 
    {
        void Start()
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            ps.GetComponent<Renderer>().sortingLayerName = "Particles";
        }
    }
}
