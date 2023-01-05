using Nico.Interface;

namespace Games.CricketGame.Code.Pokemon
{
    public class Controller : ICoreComponent
    {
        private Cricket _cricket;
        private InputHandler _handler => _cricket.handler;


        public Controller(Cricket cricket)
        {
            _cricket = cricket;
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