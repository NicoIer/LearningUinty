namespace Games.CricketGame.Code.Pokemon.Skill
{
    public interface ISkillEffect
    {
        public void Apply(Pokemon user, Pokemon hitter);
    }
}