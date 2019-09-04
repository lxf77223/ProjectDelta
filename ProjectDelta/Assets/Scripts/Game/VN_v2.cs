using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VN_v2 : MonoBehaviour
{
    /// <summary>
    /// 枚举
    /// </summary>
    PlayerState playerState;
    Dir attckDir;

    /// <summary>
    /// 组件
    /// </summary>
    Animator anim;
    Transform playerPos;
    Rigidbody2D my_Rigidbody2D;
    AnimatorStateInfo animatorStateInfo;

    /// <summary>
    /// 动画状态
    /// </summary>
    bool isIdle = false;
    bool isRun = false;
    bool isLand = false;//是否在地上
    bool isJump = false;//是否跳跃
    bool isStay = false;//是否蹲下
    bool isAttack = false;//是否攻击
    bool isShot; //是否射击过一次
    bool isSkill_E = false;//是否在使用E技能
    /// <summary>
    /// 变量
    /// </summary>
    float h;//横轴按键映射
    float speedY;//刚体Y轴速度

    /// <summary>
    /// 需要面板调控的变量
    /// </summary>
    public float moveForece = 3.0f;//移动的力
    public float jumpForce = 450f;//人物跳跃的力
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerPos = GetComponent<Transform>();
        my_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //需要持续获取的变量
        h = Input.GetAxisRaw("Horizontal");
        speedY = my_Rigidbody2D.velocity.y;

        print(playerState);

        if (h!=0)
        {
            turn();
        }
        SetAnim();
        PlayerStateControl();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.activeInHierarchy)
        {
            isLand = true;
        }
    }
    /// <summary>
    /// 碰撞物体离开时
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.activeInHierarchy)
        {
            isLand = false;
        }
    }
    /// <summary>
    /// 设置动画机
    /// </summary>
    private void SetAnim()
    {
        anim.SetBool("IsRun", isRun);
        anim.SetBool("IsIdle", isIdle);
        anim.SetBool("IsJump", isJump);
        anim.SetFloat("SpeedY",speedY);
        anim.SetBool("isLand", isLand);
    }
    /// <summary>
    /// 状态机
    /// </summary>
    private void PlayerStateControl()
    {
        if (h == 0)
        {
            isRun = false;
            isJump = false;
            playerState = PlayerState.PS_Idle;
        }
        if (h != 0)
        {
            if (playerState == PlayerState.PS_Idle)
            {
                isIdle = false;
                isRun = true;
                playerState = PlayerState.PS_Run;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isIdle = false;
            isRun = false;
            playerState = PlayerState.PS_Jump;
        }
        //if (isLand && playerState == PlayerState.PS_Jump)
        //{
        //    isJump = false;
        //    playerState = PlayerState.PS_Idle;
        //}
        switch (playerState)    
        {
            case PlayerState.PS_Idle:
                Idle();
                break;
            case PlayerState.PS_Run:
                Move();
                break;
            case PlayerState.PS_Attack:
                break;
            case PlayerState.PS_Jump:
                Jump();
                Move();
                break;
            case PlayerState.PS_Stay:
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 人物站立
    /// </summary>
    private void Idle()
    {
        isIdle = true;
        my_Rigidbody2D.velocity =Vector2.zero;
    }
    /// <summary>
    /// 人物移动
    /// </summary>
    private void Move()
    {
        //向右移动
        if (h == 1)
        {
            my_Rigidbody2D.velocity = new Vector2(h * moveForece, speedY);
        }
        //向左移动
        else if (h == -1)
        {
            my_Rigidbody2D.velocity = new Vector2(h * moveForece, speedY);
        }
    }
    /// <summary>
    /// 人物转向
    /// </summary>
    private void turn()
    {
        //右边
        if (h == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            attckDir = Dir.Right;
        }
        //左边
        if (h == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            attckDir = Dir.Left;
        }
    }
    /// <summary>
    /// 人物跳跃
    /// </summary>
    private void Jump()
    {
        if (isJump == false)
        {
            isJump = true;
            //给人物向上的力
            my_Rigidbody2D.AddForce(new Vector2(0, 1) * jumpForce);
            //实现悬崖下跳跃 且跳跃高度不受速度影响 加强手感
            my_Rigidbody2D.velocity = new Vector2(0, 0);
        }
    }

}
