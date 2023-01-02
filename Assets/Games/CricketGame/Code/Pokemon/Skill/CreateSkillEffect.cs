using Games.CricketGame.Code.Pokemon.Enum;
using Games.CricketGame.Code.Pokemon.Skill.Effects;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon.Skill
{
    public class CreateSkillEffect : MonoBehaviour
    {
        private void Awake()
        {
            //ToDO 在这里添加所有技能效果接口表
            Skill.Add<电光一闪>(SkillEnum.电光一闪,true);//技能枚举和技能效果类型的对应
            
            Skill.Save();
        }
    }
}