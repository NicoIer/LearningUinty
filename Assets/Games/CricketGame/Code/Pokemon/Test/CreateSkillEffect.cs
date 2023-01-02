using System;
using Games.CricketGame.Code.Pokemon.Skill;
using Games.CricketGame.Code.Pokemon.Skill.Effects;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon.Test
{
    public class CreateSkillEffect : MonoBehaviour
    {
        private void Awake()
        {
            //ToDO 暂时就这样吧
            Skill.Skill.Add<电光一闪>(SkillEnum.电光一闪);//技能枚举和技能效果类型的对应
            Skill.Skill.Save();
        }
    }
}