using System;
using System.Collections;
using System.Collections.Generic;
using AttackGame;
using UnityEngine;

namespace AttackGame
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerData data;
        private Rigidbody2D _rigidbody;
        private Vector2 _velocity;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _velocity.x = Input.GetAxis("Horizontal") * data.move_speed;
            _velocity.y = Input.GetAxis("Vertical") * data.move_speed;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _velocity;
        }
    }
}

