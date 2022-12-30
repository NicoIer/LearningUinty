using System;
using System.Collections;
using System.Collections.Generic;
using AttackGame;
using AttackGame.common.component;
using UnityEngine;

namespace AttackGame.Player
{
    public class PlayerController: ICoreComponent
    {
        #region Construct

        public PlayerController(Player player)
        {
            _player = player;
        }

        #endregion

        #region Private Attribute

        private readonly Player _player;

        #endregion

        #region Public Attribute

        private Vector2 _velocity;
        public Vector2 velocity => _velocity;

        #endregion

        #region Reference Attribute

        private Rigidbody2D rigidbody => _player.rigidbody2D;
        private PlayerInputHandler handler => _player.handler;

        #endregion

        public void Start()
        {
            
        }

        public void Update()
        {
            _velocity.x = handler.move.x * _player.data.move_speed;
            _velocity.y = handler.move.y * _player.data.move_speed;
        }
        

        public void FixedUpdate()
        {
            rigidbody.velocity = velocity;
        }
    }
}

