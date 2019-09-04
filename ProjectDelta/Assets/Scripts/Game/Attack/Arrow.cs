using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Codroe.Projectdelta.Attack
{
    public class Arrow : MonoBehaviour
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
        bool isAttackEffect_3 = false;
        /// <summary>
        /// 变量
        /// </summary>
        Vector2 startPos;//箭生成的位置
        Vector2 currentPos;
        Vector3 arrowFlyPos;//箭的当前飞行方向 vector3类型
        float effectOffset = 0.1f;
        float dir;
        float Attack;
        /// <summary>
        /// 面板调控变量
        /// </summary>
        public float speed = 20.0f;//箭的飞行速度
        public float range = 5.0f;//箭的攻击距离
        private void Awake()
        {
            EventCenter.AddListener<Vector3, float, float>(EventDefine.ArrowAttack, ArrowAttack);
            //EventCenter.AddListener<float>(EventDefine.SetAttack, SetAttack);
            My_rigdbody2d = GetComponent<Rigidbody2D>();
        }
        private void OnDestroy()
        {
            EventCenter.RemoveListener<Vector3, float, float>(EventDefine.ArrowAttack, ArrowAttack);
            //EventCenter.RemoveListener<float>(EventDefine.SetAttack, SetAttack);
        }

        private void FixedUpdate()
        {
            currentPos = transform.position;
            arrowFlyPos = transform.localScale;
            ArrowEffect();
            AttackRange();
            switch (dir)
            {
                case 1:
                    if ((currentPos.x > startPos.x + 2f && currentPos.x < startPos.x + 3f)
                && isAttackEffect_3 == false)
                    {
                        ArrowEffect_3();
                        isAttackEffect_3 = true;
                    }
                    break;
                case -1:
                    if ((currentPos.x < startPos.x - 2f && currentPos.x > startPos.x - 3f)
                && isAttackEffect_3 == false)
                    {
                        ArrowEffect_3();
                        isAttackEffect_3 = true;
                    }
                    break;
                default:
                    break;
            }

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                isFly = false;
                isAttackEffect_3 = false;
                transform.gameObject.SetActive(false);
                collision.GetComponent<Character.CharacterStatus>().Damage(Attack);
            }
        }
        /// <summary>
        /// 箭的飞行攻击
        /// </summary>
        private void ArrowAttack(Vector3 startPos, float dir, float chAttack)
        {
            if (isFly == false && transform.gameObject.activeInHierarchy)
            {
                My_rigdbody2d.velocity = new Vector2(dir, 0) * speed;
                Attack = chAttack;
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
                this.dir = dir;
                //获取到箭的起始位置
                this.startPos = startPos;
                isFly = true;
            }
        }

        /// <summary>
        /// 箭攻击距离
        /// </summary>
        private void AttackRange()
        {
            //每个方向超出范围都要消除
            //TODO : 只做了2个方向，还有其他需要的方向
            if (transform.position.x >= startPos.x + range || transform.position.x <= startPos.x - range)
            {
                isFly = false;
                isAttackEffect_3 = false;
                transform.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 箭追尾特效
        /// </summary>
        private void ArrowEffect()
        {
            EventCenter.Broadcast(EventDefine.CreatArrowEffect, currentPos, arrowFlyPos);
        }
        /// <summary>
        /// 箭中途特效
        /// </summary>
        private void ArrowEffect_3()
        {
            EventCenter.Broadcast(EventDefine.CreatArrowEffect_3, currentPos, arrowFlyPos);
        }
        /// <summary>
        /// 获取人物攻击力
        /// </summary>
        /// <param name="chAttack"></param>
        //private void SetAttack(float chAttack)
        //{
        //    Attack = chAttack;
        //}
        /// <summary>
        /// 箭的飞行
        /// 这里可以实现飞行和确定方向分离 考虑中
        /// </summary>
        //private void ArrowFly()
        //{
        //    switch (CurrentAttackDir)
        //    {
        //        case Dir.Right:
        //            My_rigdbody2d.velocity = new Vector2(1, 0) * speed;
        //            break;
        //        case Dir.Left:
        //            My_rigdbody2d.velocity = new Vector2(-1, 0) * speed;
        //            break;
        //        case Dir.Up:
        //            break;
        //        case Dir.Down:
        //            break;
        //        default:
        //            break;
        //    }
        //}


    }
}
