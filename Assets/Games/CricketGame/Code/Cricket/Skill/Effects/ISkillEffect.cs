using Cysharp.Threading.Tasks;
using Games.CricketGame.Code.Cricket_;

namespace Games.CricketGame.Cricket_
{
    public interface ISkillEffect
    {
        public UniTask Apply(Cricket attacker, Cricket defenser,Skill skill);
    }
}