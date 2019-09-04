using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Character
{
    /// <summary>
    /// 
    /// </summary>
    public class CharacterMotor : MonoBehaviour
    {
        //刚体组件
        Rigidbody2D my_Rigidbody2D;

        //人物移动的力
        public float moveForece = 3.0f;
        //人物跳跃的力
        public float jumpForce = 450f;

        private void Awake()
        {
            my_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void Turn(float h)
        {
            //人物转向
            transform.localScale = new Vector3(h, 1, 1);
        }
        public void Move(float h)
        {
            //人物移动
            my_Rigidbody2D.velocity = new Vector2(h * moveForece, my_Rigidbody2D.velocity.y);
        }
        public void Jump()
        {
            //实现悬崖下跳跃 且跳跃高度不受速度影响 加强手感
            my_Rigidbody2D.velocity = new Vector2(0, 0);
            //给人物向上的力
            my_Rigidbody2D.AddForce(new Vector2(0, 1) * jumpForce);
        }
        public void Attack()
        {
            //让人物不会因为移动的惯性而移动
            my_Rigidbody2D.velocity = new Vector2(0, my_Rigidbody2D.velocity.y);
        }
        public void FastFall()
        {
            //人物快速下落
            my_Rigidbody2D.velocity = new Vector2(my_Rigidbody2D.velocity.x, -20f);
        }
        public void Idle()
        {
            //人物停止不动
            my_Rigidbody2D.velocity = new Vector2(0, my_Rigidbody2D.velocity.y);
        }
        public void Attack_Skill_E(float currentDir)
        {
            //后坐力
            my_Rigidbody2D.velocity = new Vector2(5f * -currentDir, my_Rigidbody2D.velocity.y);
        }
    }
}