using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Manager;
using UnityEngine;

namespace Games.CricketGame.Cricket_
{
    public class 电光一闪 : ISkillEffect
    {
        public async UniTask Apply(Cricket attacker, Cricket defenser, Skill skill)
        {
            var damage = SkillManager.PhysicalInjury(attacker.data, defenser.data, skill);
            Debug.Log(damage);
            await defenser.data.ChangeHealth(damage);
        }
    }
}