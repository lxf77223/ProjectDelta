using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Character
{
    /// <summary>
    /// 角色状态
    /// </summary>
    public class PlayerStatus : CharacterStatus
    {
        protected override void Death()
        {
            base.Death();
            print("玩家死亡");
            transform.gameObject.SetActive(false);
        }
    }
}