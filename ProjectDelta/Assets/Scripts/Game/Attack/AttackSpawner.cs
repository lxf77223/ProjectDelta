using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Attack
{
    public class AttackSpawner : MonoBehaviour
    {
        /// <summary>
        /// 变量
        /// </summary>
        float attackOffsetX = 0.28f;
        float attackOffsetY = 0.05f;
        private void Awake()
        {
            EventCenter.AddListener<Vector2, float, float>(EventDefine.CreatArrow, CreatArrow);
            EventCenter.AddListener<Vector2, Vector3>(EventDefine.CreatArrowEffect, CreatArrowEffect);
            EventCenter.AddListener<Vector2, Vector3>(EventDefine.CreatArrowEffect_3, CreatArrowEffect_3);
            EventCenter.AddListener<Vector2, float>(EventDefine.CreatSkill_E, CreatSkill_E);
            EventCenter.AddListener<Vector2, Vector3>(EventDefine.CreatSkiil_E_Follow, CreatSkiil_E_Follow);
        }
        private void OnDestroy()
        {
            EventCenter.RemoveListener<Vector2, float, float>(EventDefine.CreatArrow, CreatArrow);
            EventCenter.RemoveListener<Vector2, Vector3>(EventDefine.CreatArrowEffect, CreatArrowEffect);
            EventCenter.RemoveListener<Vector2, Vector3>(EventDefine.CreatArrowEffect_3, CreatArrowEffect_3);
            EventCenter.RemoveListener<Vector2, float>(EventDefine.CreatSkill_E, CreatSkill_E);
            EventCenter.RemoveListener<Vector2, Vector3>(EventDefine.CreatSkiil_E_Follow, CreatSkiil_E_Follow);
        }
        /// <summary>
        /// 生成箭
        /// </summary>
        private void CreatArrow(Vector2 currentPos, float attackDir, float chAttack)
        {
            //拿对象池的箭
            GameObject go = AttackPool.Instance.GetArrow();
            //显示箭
            go.SetActive(true);
            //生成箭的位置以及箭的方向
            go.transform.position = new Vector2(currentPos.x + attackOffsetX * attackDir, currentPos.y + attackOffsetY);
            go.transform.localScale = new Vector3(attackDir, 1, 1);
            CreatArrowEffect_2(new Vector2(currentPos.x + 1.1f * attackDir, currentPos.y + attackOffsetY)
                      , go.transform.localScale);
            //switch (attackDir)
            //{
            //    case Dir.Right:
            //        go.transform.position = new Vector2(currentPos.x + attackOffsetX, currentPos.y + attackOffsetY);
            //        go.transform.localScale = new Vector3(1, 1, 1);
            //        CreatArrowEffect_2(new Vector2(currentPos.x + 1.1f, currentPos.y + attackOffsetY)
            //            , go.transform.localScale);
            //        //CreatArrowEffect_3(new Vector2(currentPos.x + 1.1f, currentPos.y + attackOffsetY)
            //        //   , go.transform.localScale);
            //        break;
            //    case Dir.Left:
            //        go.transform.position = new Vector2(currentPos.x - attackOffsetX, currentPos.y + attackOffsetY);
            //        go.transform.localScale = new Vector3(-1, 1, 1);
            //        CreatArrowEffect_2(new Vector2(currentPos.x - 1.1f, currentPos.y + attackOffsetY)
            //            , go.transform.localScale);
            //        //CreatArrowEffect_3(new Vector2(currentPos.x - 1.1f, currentPos.y + attackOffsetY)
            //        //   , go.transform.localScale);
            //        break;
            //    case Dir.Up:
            //        break;
            //    case Dir.Down:
            //        break;
            //    default:
            //        break;
            //}
            //广播箭的攻击
            EventCenter.Broadcast(EventDefine.ArrowAttack, go.transform.position, attackDir, chAttack);
        }
        /// <summary>
        /// 生成E技能
        /// </summary>
        private void CreatSkill_E(Vector2 currentPos, float attackDir)
        {
            //拿对象池的E技能
            GameObject go = AttackPool.Instance.GetSkill_E();
            //显示E技能
            go.SetActive(true);
            go.transform.position = new Vector2(currentPos.x + attackOffsetX * attackDir, currentPos.y + attackOffsetY);
            go.transform.localScale = new Vector3(attackDir, 1, 1);
            //生成E技能的位置以及E技能的方向
            //switch (attackDir)
            //{
            //    case Dir.Right:
            //        go.transform.position = new Vector2(currentPos.x + attackOffsetX, currentPos.y + attackOffsetY);
            //        go.transform.localScale = new Vector3(1, 1, 1);
            //        break;
            //    case Dir.Left:
            //        go.transform.position = new Vector2(currentPos.x - attackOffsetX, currentPos.y + attackOffsetY);
            //        go.transform.localScale = new Vector3(-1, 1, 1);
            //        break;
            //    case Dir.Up:
            //        break;
            //    case Dir.Down:
            //        break;
            //    default:
            //        break;
            //}
            //广播E技能的飞行攻击
            EventCenter.Broadcast(EventDefine.Skill_EAttack, go.transform.position, attackDir);
        }
        /// <summary>
        /// 生成特效
        /// </summary>
        private void CreatArrowEffect(Vector2 currentPos, Vector3 arrowFlyDir)
        {
            GameObject go = AttackPool.Instance.GetArrowEffct();
            go.transform.position = currentPos;
            go.SetActive(true);
            go.transform.localScale = arrowFlyDir;
        }

        /// <summary>
        /// 生成发射特效
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="arrowFlyDir"></param>
        private void CreatArrowEffect_2(Vector2 currentPos, Vector3 arrowFlyDir)
        {
            GameObject go = AttackPool.Instance.GetArrowEffct_2();
            go.transform.position = currentPos;
            go.SetActive(true);
            go.transform.localScale = arrowFlyDir;
        }
        /// <summary>
        /// 生成发射特效
        /// </summary>
        /// <param name="currentPos"></param>
        /// <param name="arrowFlyDir"></param>
        private void CreatArrowEffect_3(Vector2 currentPos, Vector3 arrowFlyDir)
        {
            GameObject go = AttackPool.Instance.GetArrowEffct_3();
            go.transform.position = currentPos;
            go.SetActive(true);
            go.transform.localScale = arrowFlyDir;
        }
        /// <summary>
        /// 生成E技能追尾特效
        /// </summary>
        private void CreatSkiil_E_Follow(Vector2 currentPos, Vector3 FlyDir)
        {
            GameObject go = AttackPool.Instance.GetSkill_E_Follow();
            go.transform.position = currentPos;
            go.SetActive(true);
            go.transform.localScale = FlyDir;
        }
    }
}
