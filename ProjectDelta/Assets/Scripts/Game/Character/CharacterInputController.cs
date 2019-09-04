using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Character
{
    public class CharacterInputController : MonoBehaviour
    {
        /// <summary>
        /// 枚举
        /// </summary>
        //Dir attckDir;

        /// <summary>
        /// 组件
        /// </summary>
        Animator anim;
        Rigidbody2D my_Rigidbody2D;
        AnimatorStateInfo animatorStateInfo;
        CharacterMotor chMotor;
        PlayerStatus playerStatus;

        /// <summary>
        /// 状态
        /// </summary>
        bool isLand = false;//是否在地上
        bool isRun = false;//是否跑动
        bool isStay = false;//是否蹲下
        bool isAttack = false;//是否攻击
        bool isShot; //是否射击过一次
        bool isShot_E; //是否射击过一次
        bool isSkill_E = false;//是否在使用E技能
        bool isFastfall = false;
        bool isIdle = false;

        int isJump_v2 = 0;

        /// <summary>
        /// 变量
        /// </summary>
        float speedY;//刚体Y轴速度
        Vector2 currentPos;//人物当前位置
        Vector2 jumpStartPos;//人物跳跃是最开始的位置
        float currentDir;//人物x轴的方向
        float h;//记录水平按键值

        /// <summary>
        /// 需要面板调控的变量
        /// </summary>
        public float jumpForce = 450f;//人物跳跃的力
        public float moveForece = 3.0f;

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            my_Rigidbody2D = GetComponent<Rigidbody2D>();
            chMotor = GetComponent<CharacterMotor>();
            playerStatus = GetComponent<PlayerStatus>();
        }

        void Update()
        {
            //设置持续改变的变量
            currentPos = transform.position;
            currentDir = transform.localScale.x;
            speedY = my_Rigidbody2D.velocity.y;//记录y轴速度
            h = Input.GetAxisRaw("Horizontal");

            //设置动画参数
            SetChAnimParameters();

            //必须在update中获取，用于判断动画状态
            animatorStateInfo = anim.GetCurrentAnimatorStateInfo(0);


            //函数调用
            //转向指令
            if (h != 0 && isAttack == false && isSkill_E == false)
            {
                OnTurn();
            }

            //原地不动指令
            if (isSkill_E == false
                && isRun == false
                && isStay == false
                && isAttack == false
                && isJump_v2 == 0 && isLand)
            {
                OnIdle();  
            }
            else
            {
                isIdle = false;
            }

            //跳跃指令
            if (isStay == false && isAttack == false)
            {
                if (Input.GetKeyDown(KeyCode.Space) && isJump_v2 == 0)
                {
                    OnJump();
                }
                //不能在地面上增加跳跃次数，在打断攻击后摇时有bug
                //松开空格，加1跳跃次数，可以进行二段跳
                if (Input.GetKeyUp(KeyCode.Space) && isLand == false)
                {
                    isJump_v2++;
                }
                if (Input.GetKeyDown(KeyCode.Space) && isJump_v2 == 1)
                {
                    OnJump();
                }
            }

            //快速下落指令
            if (isJump_v2 > 0)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    isFastfall = true;
                }
            }

            //移动指令
            if (isStay == false && isAttack == false && isSkill_E == false)
            {
                if ((h == 1 || h == -1))
                {
                    isRun = true;
                }
                //停止移动时
                else
                {
                    isRun = false;
                }
            }


            //攻击指令
            if (isAttack == false && isStay == false && isSkill_E == false)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    isAttack = true;
                    //打断跑步
                    isRun = false;
                }
            }
            //E技能指令
            if (isSkill_E == false && isStay == false)
            {
                if (Input.GetKeyDown(KeyCode.U))
                {
                    isSkill_E = true;
                    //打断跑步
                    isRun = false;
                    //打断攻击
                    isAttack = false;
                }
            }
            //在地上的动作
            if (isLand)
            {
                //蹲下指令
                if (Input.GetKey(KeyCode.S))
                {
                    //打断跑步
                    isRun = false;
                    isStay = true;
                }
                if (Input.GetKeyUp(KeyCode.S))
                {
                    isStay = false;
                }
            }



            if (isRun)
            {
                OnMove();
            }
            if (isStay)
            {
                Stay();
            }
            if (isAttack)
            {
                OnAttack();
            }
            if (isFastfall)
            {
                OnFastFall();
            }
            if (isSkill_E)
            {
                Attack_Skill_E();
            }

        }
        private void SetChAnimParameters()
        {
            //将动画机参数放在一个类里保存，方便修改参数名
            anim.SetBool(playerStatus.chAnimParameters.IsLand, isLand);
            anim.SetBool(playerStatus.chAnimParameters.IsRun, isRun);
            anim.SetBool(playerStatus.chAnimParameters.IsStay, isStay);
            anim.SetBool(playerStatus.chAnimParameters.IsAttack, isAttack);
            anim.SetBool(playerStatus.chAnimParameters.IsSkill_E, isSkill_E);
            anim.SetBool(playerStatus.chAnimParameters.ISFastfall, isFastfall);
            anim.SetBool(playerStatus.chAnimParameters.IsIdle, isIdle);
            anim.SetFloat(playerStatus.chAnimParameters.SpeedY, speedY);
            anim.SetInteger(playerStatus.chAnimParameters.IsJump_v2, isJump_v2);
        }
        /// <summary>
        /// 碰撞到物体时
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                isLand = true;
                isFastfall = false;
                isJump_v2 = 0;
            }
        }
        /// <summary>
        /// 碰撞物体离开时
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                isLand = false;
            }
        }
        /// <summary>
        /// 人物转向
        /// </summary>
        private void OnTurn()
        {

            chMotor.Turn(h);
        }

        private void OnIdle()
        {
            isIdle = true;
            chMotor.Idle();
        }

        /// <summary>
        /// 人物移动
        /// </summary>
        /// <param name="h">移动方向</param>
        private void OnMove()
        {
            chMotor.Move(h);
        }
        /// <summary>
        /// 人物跳跃
        /// </summary>
        private void OnJump()
        {
            chMotor.Jump();
        }
        /// <summary>
        /// 人物蹲下
        /// </summary>
        private void Stay()
        {
            //在地上才能蹲下

            //my_Rigidbody2D.velocity = new Vector2(0, speedY);

        }
        /// <summary>
        /// 人物攻击
        /// </summary>
        private void OnAttack()
        {
            //暂时使用方式3
            //方式1：按下可以让进入攻击状态，然后通过攻击动画判断，如果人物不能通过其他动画取消攻击动画，
            //      等到发射完普通攻击的时候才可以进行其他动作
            //方式2：按下可以让进入攻击状态，然后通过攻击动画判断，如果人物通过其他动画取消攻击动画，则不会发射箭，松开取消攻击状态，
            //      即实现了长按可以持续发射，也可以取消攻击
            //方式3：按下可以让进入攻击状态，等待攻击动画结束的时候会才可以进行其他基础动作。
            chMotor.Attack();
            if ((animatorStateInfo.normalizedTime > 0.3)
                && (animatorStateInfo.IsName("Vn_Attack3")))
            {
                //需要判断角色是否是第一次射击，否则因为animatorStateInfo.normalizedTime不安定，导致连续射箭或者射不出箭
                if (isShot == false)
                {
                    //广播创造箭
                    EventCenter.Broadcast(EventDefine.CreatArrow, currentPos, currentDir,playerStatus.baseATK);
                    isShot = true;
                }
                //isAttack = false;
            }
            else
            {
                isShot = false;
            }
            if ((animatorStateInfo.normalizedTime > 0.9)
                && (animatorStateInfo.IsName("Vn_Attack3")))
            {
                isAttack = false;
            }

            //if (Input.GetKeyUp(KeyCode.J) && isLand && isAttack)
            //{
            //    isAttack = false;
            //}
        }
        /// <summary>
        /// 使用E技能
        /// </summary>
        private void Attack_Skill_E()
        {
            if ((animatorStateInfo.normalizedTime > 0.3)
                && (animatorStateInfo.IsName("Vn_Skill_E")))
            {
                //需要判断角色是否是第一次射击，否则因为animatorStateInfo.normalizedTime不安定，导致连续射箭或者射不出箭
                if (isShot_E == false)
                {
                    //广播创造E技能
                    EventCenter.Broadcast(EventDefine.CreatSkill_E, currentPos, currentDir);
                    isShot_E = true;
                    chMotor.Attack_Skill_E(currentDir);
                }
                //isAttack = false;
            }
            else
            {
                isShot_E = false;
            }

            if ((animatorStateInfo.normalizedTime > 0.9)
                && (animatorStateInfo.IsName("Vn_Skill_E")))
            {
                isSkill_E = false;
            }
        }
        private void OnFastFall()
        {
            chMotor.FastFall();
        }
    }
}
