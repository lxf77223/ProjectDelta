using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Attack
{
    public class Skill_E : MonoBehaviour
    {
        /// <summary>
        /// 枚举
        /// </summary>
        // 发射出去的箭此时的方向
        Dir CurrentAttackDir;

        /// <summary>
        /// 组件
        /// </summary>
        Rigidbody2D My_rigdbody2d;

        /// <summary>
        /// 状态
        /// </summary>
        bool isFly = false;

        /// <summary>
        /// 变量
        /// </summary>
        Vector2 startPos;//箭生成的位置
        Vector2 currentPos;
        Vector3 arrowFlyPos;//箭的当前飞行方向 vector3类型
        float effectOffset = 0.1f;

        /// <summary>
        /// 面板调控变量
        /// </summary>
        public float speed = 20.0f;//箭的飞行速度
        public float range = 5.0f;//箭的攻击距离
        private void Awake()
        {
            EventCenter.AddListener<Vector3, float>(EventDefine.Skill_EAttack, Skill_EAttack);
            My_rigdbody2d = GetComponent<Rigidbody2D>();
        }
        private void OnDestroy()
        {
            EventCenter.RemoveListener<Vector3, float>(EventDefine.Skill_EAttack, Skill_EAttack);
        }
        private void FixedUpdate()
        {
            currentPos = transform.position;
            arrowFlyPos = transform.localScale;
            Skill_EEffect();
            Skill_ERange();


        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                isFly = false;
                transform.gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// E技能的飞行攻击
        /// </summary>
        private void Skill_EAttack(Vector3 startPos, float dir)
        {
            if (isFly == false && transform.gameObject.activeInHierarchy)
            {
                My_rigdbody2d.velocity = new Vector2(dir, 0) * speed;
                //switch (dir)
                //{
                //    case Dir.Right:
                //        My_rigdbody2d.velocity = new Vector2(1, 0) * speed;
                //        break;
                //    case Dir.Left:
                //        My_rigdbody2d.velocity = new Vector2(-1, 0) * speed;
                //        break;
                //    case Dir.Down:
                //        break;
                //    case Dir.Up:
                //        break;
                //    default:
                //        break;
                //}
                //CurrentAttackDir = dir;
                //获取到箭的起始位置
                this.startPos = startPos;
                isFly = true;
            }
        }
        /// <summary>
        /// E技能攻击距离
        /// </summary>
        private void Skill_ERange()
        {
            //TODO 因为使用对象池进行生成，所以箭的起始生成位置要重新拿
            //每个方向超出范围都要消除
            //TODO : 只做了2个方向，还有其他需要的方向
            if (transform.position.x >= startPos.x + range || transform.position.x <= startPos.x - range)
            {
                isFly = false;
                transform.gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// E技能追尾特效
        /// </summary>
        private void Skill_EEffect()
        {
            EventCenter.Broadcast(EventDefine.CreatSkiil_E_Follow, currentPos, arrowFlyPos);
        }
    }
}
