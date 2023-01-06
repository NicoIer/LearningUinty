using Games.CricketGame.Code.Cricket_;

namespace Games.CricketGame.Cricket_
{
    public interface ISkillEffect
    {
        public void Apply(Cricket user, Cricket hitter,Skill skill);
    }
}