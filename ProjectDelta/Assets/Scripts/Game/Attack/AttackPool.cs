using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Attack
{
    public class AttackPool : MonoBehaviour
    {
        /// <summary>
        /// 单例模式
        /// </summary>
        public static AttackPool Instance;
        /// <summary>
        /// 资源管理器
        /// </summary>
        ManagerVars vars;
        /// <summary>
        /// List
        /// </summary>
        private List<GameObject> arrowList = new List<GameObject>();
        private List<GameObject> arrowEffectList = new List<GameObject>();
        private List<GameObject> arrowEffectList_2 = new List<GameObject>();
        private List<GameObject> arrowEffectList_3 = new List<GameObject>();
        private List<GameObject> skill_EList = new List<GameObject>();
        private List<GameObject> skill_E_FollowList = new List<GameObject>();
        /// <summary>
        /// 变量
        /// </summary>
        public int initCount;//每个List中的物体数

        private void Awake()
        {
            Instance = this;
            vars = ManagerVars.GetManagerVars();
            Init();
        }
        /// <summary>
        /// 初始化一个物体的方法
        /// </summary>
        /// <param name="prefab">需要创建的预制体</param>
        /// <param name="addlist">需要添加的list</param>
        /// <returns></returns>
        //使用ref关键字来对List进行修改
        private GameObject InstanitiateObject(GameObject prefab, ref List<GameObject> addlist)
        {
            //将生成的物体放在池子里
            GameObject go = Instantiate(prefab, transform);
            //生成出来的物体都隐藏
            go.SetActive(false);
            //将生成的物体放到对应的List中
            addlist.Add(go);
            //为什么要做返回值？
            //因为在对象池中没有可以使用的物体的时候，需要通过该方法新建一个物体，然后再传到游戏内
            return go;
        }
        /// <summary>
        /// 初始化所有list的方法
        /// </summary>
        private void Init()
        {
            //初始化箭
            for (int i = 0; i < initCount; i++)
            {
                InstanitiateObject(vars.arrowPre, ref arrowList);
            }
            //初始化特效
            for (int i = 0; i < initCount; i++)
            {
                InstanitiateObject(vars.arrowEfectPre, ref arrowEffectList);
            }
            //初始化特效
            for (int i = 0; i < initCount; i++)
            {
                InstanitiateObject(vars.arrowEffectPre_2, ref arrowEffectList_2);
            }
            //初始化特效
            for (int i = 0; i < initCount; i++)
            {
                InstanitiateObject(vars.arrowEffectPre_3, ref arrowEffectList_3);
            }
            //初始化E技能
            for (int i = 0; i < initCount; i++)
            {
                InstanitiateObject(vars.skill_EPre, ref skill_EList);
            }
            //初始化E技能追尾特效
            for (int i = 0; i < initCount; i++)
            {
                InstanitiateObject(vars.skill_E_FollowPre, ref skill_E_FollowList);
            }
        }
        /// <summary>
        /// 获取箭的方法
        /// </summary>
        /// <returns></returns>
        public GameObject GetArrow()
        {
            for (int i = 0; i < arrowList.Count; i++)
            {
                //如果List中的物体没有启用
                //说明该物体可以使用
                if (arrowList[i].activeInHierarchy == false)
                {
                    return arrowList[i];
                }
            }
            return InstanitiateObject(vars.arrowPre, ref arrowList);
        }
        /// <summary>
        /// 获取箭特效的方法
        /// </summary>
        /// <returns></returns>
        public GameObject GetArrowEffct()
        {
            for (int i = 0; i < arrowEffectList.Count; i++)
            {
                if (arrowEffectList[i].activeInHierarchy == false)
                {
                    return arrowEffectList[i];
                }
            }
            return InstanitiateObject(vars.arrowEfectPre, ref arrowEffectList);
        }
        /// <summary>
        /// 获取箭特效的方法
        /// </summary>
        /// <returns></returns>
        public GameObject GetArrowEffct_2()
        {
            for (int i = 0; i < arrowEffectList_2.Count; i++)
            {
                if (arrowEffectList_2[i].activeInHierarchy == false)
                {
                    return arrowEffectList_2[i];
                }
            }
            return InstanitiateObject(vars.arrowEffectPre_2, ref arrowEffectList_2);
        }
        /// <summary>
        /// 获取箭特效的方法
        /// </summary>
        /// <returns></returns>
        public GameObject GetArrowEffct_3()
        {
            for (int i = 0; i < arrowEffectList_3.Count; i++)
            {
                if (arrowEffectList_3[i].activeInHierarchy == false)
                {
                    return arrowEffectList_3[i];
                }
            }
            return InstanitiateObject(vars.arrowEffectPre_3, ref arrowEffectList_3);
        }
        /// <summary>
        /// 获取E技能的方法
        /// </summary>
        /// <returns></returns>
        public GameObject GetSkill_E()
        {
            for (int i = 0; i < skill_EList.Count; i++)
            {
                if (skill_EList[i].activeInHierarchy == false)
                {
                    return skill_EList[i];
                }
            }
            return InstanitiateObject(vars.skill_EPre, ref skill_EList);
        }
        /// <summary>
        /// 获取箭特效的方法
        /// </summary>
        /// <returns></returns>
        public GameObject GetSkill_E_Follow()
        {
            for (int i = 0; i < skill_E_FollowList.Count; i++)
            {
                if (skill_E_FollowList[i].activeInHierarchy == false)
                {
                    return skill_E_FollowList[i];
                }
            }
            return InstanitiateObject(vars.skill_E_FollowPre, ref skill_E_FollowList);
        }
    }
}
