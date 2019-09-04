using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Character
{ 
	/// <summary>
	/// 
	/// </summary>
	public class CharacterStatus : MonoBehaviour
	{
        [Tooltip("角色动画参数")]
        public CharacterAnimatorParameters chAnimParameters;
        [Tooltip("当前生命值")]
        public float HP;
        [Tooltip("最大生命值")]
        public float maxHP;
        [Tooltip("基础攻击力")]
        public float baseATK;
        [Tooltip("防御力")]
        public float defence;

        public void Damage(float val)
        {
            val -= defence;
            if (val <= 0) return;
            HP -= val;
            if (HP <= 0)
            {
                Death();
            }
        }
        protected virtual void Death()
        {
            //执行死亡动画
        }
    }
}