using System;
using System.Collections;
using System.Collections.Generic;
using PackageGame;
using PackageGame.common.component;
using UnityEngine;

namespace PackageGame.Player
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

        private Vector3 _velocity;
        public Vector3 velocity => _velocity;

        #endregion

        #region Reference Attribute

        private Rigidbody rigidbody => _player.body;
        private PlayerInputHandler handler => _player.handler;

        #endregion

        public void Start()
        {
            
        }

        public void Update()
        {
            _velocity.x = handler.move.x * _player.data.move_speed;
            _velocity.z = handler.move.y * _player.data.move_speed;
        }
        

        public void FixedUpdate()
        {
            rigidbody.velocity = velocity;
        }
    }
}

