using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Attack
{
    public class AttackEffect_Particle : MonoBehaviour
    {

        ParticleSystem ps;
        private void Awake()
        {
            ps = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            if (ps.isStopped)
            {
                transform.gameObject.SetActive(false);
            }
        }
    }
}
