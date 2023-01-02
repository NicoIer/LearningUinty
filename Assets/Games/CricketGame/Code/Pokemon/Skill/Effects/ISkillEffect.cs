namespace Games.CricketGame.Code.Pokemon.Skill.Effects
{
    public interface ISkillEffect
    {
        public void Apply(Pokemon user, Pokemon hitter);
    }
}