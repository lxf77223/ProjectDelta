using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Codroe.Projectdelta.Character
{
    /// <summary>
    /// 角色动画参数
    /// </summary>
    [System.Serializable]//序列化 将当前对象“嵌入”到脚本后，可以在编译器中显示属性
    public class CharacterAnimatorParameters 
    {
        public string IsLand = "IsLand";
        public string SpeedY = "SpeedY";
        public string IsRun = "IsRun";
        public string IsStay = "IsStay";
        public string IsAttack = "IsAttack";
        public string IsSkill_E = "IsSkill_E";
        public string IsJump_v2 = "IsJump_v2";
        public string ISFastfall = "ISFastfall";
        public string IsIdle = "IsIdle";
        
    }
}
