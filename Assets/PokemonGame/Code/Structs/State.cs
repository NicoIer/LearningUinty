namespace PokemonGame
{
    public enum StateEnum
    {
        None,
        Poisoning,
        Sleeping
    }

    public class State
    {
        public static State find_state(StateEnum stateEnum)
        {
            return new State(stateEnum.ToString(), stateEnum);
        }

        State(string name, StateEnum stateEnum)
        {
            this.name = name;
            this.stateEnum = stateEnum;
        }

        public string name;
        public StateEnum stateEnum;
    }
}