using System;
using System.Collections.Generic;
using Games.CricketGame.Cricket_;
using Nico.Interface;
using Nico.Player;
using UnityEngine;

namespace Games.CricketGame.Player_
{
    public class Player : MonoBehaviour
    {
        //玩家携带cricket
        public List<CricketData> crickets;

        //玩家拥有的Cricket
        // public List<CricketData> 
        //玩家的首发cricket
        public CricketData first_cricket { get; private set; }

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

        public CricketData FirstAvailableCricket()
        {
            foreach (var cricketData in crickets)
            {
                if (cricketData.healthAbility > 0)
                {
                    return cricketData;
                }
            }

            return null;
        }

        /// <summary>
        /// 玩家是否还有可以战斗的cricket
        /// </summary>
        /// <returns></returns>
        public bool HavaAvailableCricket()
        {
            foreach (var cricketData in crickets)
            {
                if (cricketData.healthAbility > 0)
                {
                    return true;
                }
            }

            return false;
        }

        #region Unity LifeTime

        private void Awake()
        {
            //ToDo 删掉这里
            int i = 0;
            foreach (var cricket in crickets)
            {
                cricket.RandomInit(40);
                cricket.name = $"测试cricket{i}";
                i++;
            }

            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _collider = GetComponent<Collider2D>();
            _handler = transform.Find("InputHandler").GetComponent<PlayerInputHandler>();

            _colliderHandler = new ColliderHandler(this);

            var playerController = new PlayerController(_rigidbody, _handler, physicsData);
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