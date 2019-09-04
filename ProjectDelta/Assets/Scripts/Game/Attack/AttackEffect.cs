using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Attack
{
    public class AttackEffect : MonoBehaviour
    {

        Animator anim;
        AnimatorStateInfo animatorStateInfo;
        private void Awake()
        {
            anim = transform.GetComponent<Animator>();
        }

        void Update()
        {
            animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if ((animatorStateInfo.normalizedTime > 0.9f) && (animatorStateInfo.IsName("Effect")))
            {
                transform.gameObject.SetActive(false);

            }
            if ((animatorStateInfo.normalizedTime > 0.9f) && (animatorStateInfo.IsName("Effect_2")))
            {
                transform.gameObject.SetActive(false);
            }
            if ((animatorStateInfo.normalizedTime > 0.9f) && (animatorStateInfo.IsName("Effect_3")))
            {
                transform.gameObject.SetActive(false);
            }
            if ((animatorStateInfo.normalizedTime > 0.9f) && (animatorStateInfo.IsName("Skill_E_Follow")))
            {
                transform.gameObject.SetActive(false);
            }
        }
    }
}
