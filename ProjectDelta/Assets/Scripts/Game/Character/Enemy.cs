using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Character
{
    public class Enemy : MonoBehaviour
    {
        /// <summary>
        /// 组件
        /// </summary>
        Rigidbody2D my_Rigidbody2D;
        Animator anim;
        AnimatorStateInfo animatorStateInfo;
        /// <summary>
        /// 状态
        /// </summary>
        bool isHit = false;


        private void Awake()
        {
            my_Rigidbody2D = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<Animator>();
        }
        private void FixedUpdate()
        {
            //设置动画机参数
            anim.SetBool("IsHit", isHit);

            //必须在update中获取，用于判断动画状态
            animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);

            if (isHit)
            {
                Hit();
            }

        }
        private void Hit()
        {
            if (animatorStateInfo.normalizedTime > 0.5f && animatorStateInfo.IsName("Enemy_Hit"))
            {
                isHit = false;
            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //被普通攻击箭击中
            if (collision.gameObject.tag == "Arrow")
            {
                my_Rigidbody2D.AddForce(new Vector2(50f * collision.transform.localScale.x, 0));
                isHit = true;
            }
            //被E技能击中
            if (collision.gameObject.tag == "Skill_E")
            {
                my_Rigidbody2D.AddForce(new Vector2(500f * collision.transform.localScale.x, 0));
                isHit = true;
            }
        }
    }
}
