using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Manager;
using UnityEngine;

namespace Games.CricketGame.Cricket_
{
    public class 电光一闪 : ISkillEffect
    {

        public void Apply(Cricket attacker, Cricket defenser, Skill skill)
        {
            Debug.Log($"{attacker.data.name}对{defenser.data.name}使用{skill.meta.name}");
            var damage = SkillManager.PhysicalInjury(attacker.data, defenser.data, skill);
            defenser.data.ChangeHealth(damage);
            Debug.Log($"造成{damage}点上海");
        }
    }
}