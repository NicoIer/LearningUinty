using System;
using System.Collections.Generic;
using PackageGame._Item;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PackageGame.Player
{
    public class Player : MonoBehaviour
    {
        #region Inspector

        [Header("inspector")] public PlayerData data;

        public Rigidbody body { get; private set; }

        #endregion

        #region Components

        private List<Component> _components = new();

        #endregion

        #region Reference attribute

        public Vector2 velocity => controller.velocity;
        public float health => info.health;
        public Dictionary<uint,ItemPair> items => info.items;
        #endregion

        #region Public attribute

        public PlayerInputHandler handler { get; private set; }
        public PlayerController controller { get; private set; }
        public PlayerInfo info { get; private set; }

        #endregion

        #region Private attribute
        
        #endregion

        #region Unity Callback

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            controller = new PlayerController(this);
            info = new PlayerInfo();
            if (handler == null)
            {
                handler = transform.Find("input").GetComponent<PlayerInputHandler>();
                if (handler == null)
                    throw new NullReferenceException();
            }
        }

        private void Start()
        {
            info.Start();
        }

        private void Update()
        {
            controller.Update();
            info.Update();
        }

        private void FixedUpdate()
        {
            controller.FixedUpdate();
            info.FixedUpdate();
        }

        #endregion
    }
}