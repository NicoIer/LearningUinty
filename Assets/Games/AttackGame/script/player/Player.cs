using System;
using System.Collections.Generic;
using AttackGame._Item;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AttackGame.Player
{
    public class Player : MonoBehaviour
    {
        #region Inspector

        [Header("inspector")] public PlayerData data;

        public Rigidbody2D rigidbody { get; private set; }

        #endregion

        #region Components

        private List<Component> _components = new();

        #endregion

        #region Reference attribute

        public Vector2 velocity => controller.velocity;
        public float health => playerInfo.health;
        public List<Item> items => playerInfo.items;

        #endregion

        #region Public attribute

        public PlayerInputHandler handler { get; private set; }
        public PlayerController controller { get; private set; }
        public PlayerInfo playerInfo { get; private set; }

        #endregion

        #region Private attribute

        #endregion

        #region Unity Callback

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            controller = new PlayerController(this);
            playerInfo = new PlayerInfo();
            if (handler == null)
            {
                handler = transform.Find("input").GetComponent<PlayerInputHandler>();
                if (handler == null)
                    throw new NullReferenceException();
            }
        }

        private void Update()
        {
            controller.Update();
        }

        private void FixedUpdate()
        {
            controller.FixedUpdate();
        }

        #endregion
    }
}