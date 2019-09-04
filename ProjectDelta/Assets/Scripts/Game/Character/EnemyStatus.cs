using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Character
{
    public class EnemyStatus : CharacterStatus
    {
        protected override void Death()
        {
            //执行死亡动画,游戏物体消失
            base.Death();
            print("敌人死亡");
            transform.gameObject.SetActive(false);
        }
    }
}
