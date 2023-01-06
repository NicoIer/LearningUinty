using System;
using System.Collections.Generic;
using Games.CricketGame.Code.Cricket_;
using Nico.Interface;
using Nico.Player;
using UnityEngine;
namespace Games.CricketGame.Player_
{
    public class Player : MonoBehaviour
    {
        public 
            Cricket cricket;
        
        #region Attribute

        private PlayerInputHandler _handler;
        [SerializeField] private PlayerPhysicsData physicsData;
        private ColliderHandler _colliderHandler;

        #endregion
        
        #region Unity Components

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private Collider2D _collider;
        
        #endregion

        #region Action

        public Action<Collider2D> triggerEnter2D;
        public Action<Collider2D> triggerExit2D;
        public Action<Collision2D> collisionEnter2D;
        public Action<Collision2D> collisionExit2D;

        #endregion
        
        #region CoreComponents
        
        private readonly List<ICoreComponent> _components = new();
        
        #endregion

        #region Unity LifeTime

        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
            _handler = transform.Find("InputHandler").GetComponent<PlayerInputHandler>();

            _colliderHandler = new ColliderHandler(this);
            
            var playerController = new PlayerController(_rigidbody, _handler,physicsData);
            _components.Add(playerController);
        }

        private void Start()
        {
            //组件更新
            foreach (var component in _components)
            {
                component.Start();
            }
        }

        private void Update()
        {
            //组件更新
            foreach (var component in _components)
            {
                component.Update();
            }
        }

        private void FixedUpdate()
        {
            foreach (var component in _components)
            {
                component.FixedUpdate();
            }
        }
        #endregion

        #region Trigger2D Event
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            triggerEnter2D.Invoke(col);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            
            triggerExit2D.Invoke(other);
        }

        #endregion

        #region Collider2D Event

        private void OnCollisionEnter2D(Collision2D col)
        {
            collisionEnter2D.Invoke(col);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            collisionExit2D.Invoke(other);
        }

        #endregion
        
    }
}