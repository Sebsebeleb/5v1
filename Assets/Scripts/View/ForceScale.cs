using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.View
{
    public class ForceScale : MonoBehaviour
    {
        public void Update()
        {
            Vector3 newVec = transform.localScale;
            newVec.x = 1.0f;

            transform.localScale = newVec;
        }
    }
}
