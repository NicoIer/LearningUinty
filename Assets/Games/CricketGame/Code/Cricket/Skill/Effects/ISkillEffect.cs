namespace Games.CricketGame.Manager.Code.Pokemon.Skill.Effects
{
    public interface ISkillEffect
    {
        public void Apply(Cricket user, Cricket hitter,Skill skill);
    }
}