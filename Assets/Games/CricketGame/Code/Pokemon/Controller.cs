using Nico.Interface;

namespace Games.CricketGame.Code.Pokemon
{
    public class Controller : ICoreComponent
    {
        private Pokemon _pokemon;
        private PokemonInputHandler _handler => _pokemon.handler;


        public Controller(Pokemon pokemon)
        {
            _pokemon = pokemon;
        }

        public void Start()
        {
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }
    }
}