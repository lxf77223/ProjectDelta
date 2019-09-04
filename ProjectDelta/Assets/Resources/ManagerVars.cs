using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源管理器
/// </summary>
//[CreateAssetMenu(menuName = "1")] 标签用于创建asset文件
public class ManagerVars : ScriptableObject
{
    /// <summary>
    /// 使用静态方法来读取资源管理器
    /// </summary>
    /// <returns></returns>
   public static ManagerVars GetManagerVars()
    {
        //将资源管理器放入resources文件夹，可以使用Resources方法来读取
        return Resources.Load<ManagerVars>("ManagerVarsContainer");
    }

    /// <summary>
    /// 预制体
    /// </summary>
    public GameObject arrowPre;//箭预制体
    public GameObject arrowEfectPre;//箭特效预制体
    public GameObject arrowEffectPre_2;
    public GameObject arrowEffectPre_3;
    public GameObject skill_EPre;//E技能预制体
    public GameObject skill_E_FollowPre;//E技能追尾特效预制体
}
