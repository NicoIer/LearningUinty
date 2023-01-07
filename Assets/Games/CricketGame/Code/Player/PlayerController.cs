using Nico.Interface;
using Nico.Player;
using UnityEngine;

namespace Games.CricketGame.Player_
{
    public class PlayerController: ICoreComponent
    {
        private Rigidbody2D _rigidbody;
        private PlayerInputHandler _handler;
        private PlayerPhysicsData _physicsData;
        private Vector2 _velocity;
        
        public PlayerController(Rigidbody2D rigidbody,PlayerInputHandler handler,PlayerPhysicsData physicsData)
        {
            _rigidbody = rigidbody;
            _handler = handler;
            _physicsData = physicsData;
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            _velocity.x = _handler.x * _physicsData.xSpeed;
            if (_handler.jump)
            {
                _velocity.y = _physicsData.jumpSpeed;
            }
            else
            {
                _velocity.y = _rigidbody.velocity.y;
            }
        }

        public void FixedUpdate()
        {
            _rigidbody.velocity = _velocity;
        }
    }
}