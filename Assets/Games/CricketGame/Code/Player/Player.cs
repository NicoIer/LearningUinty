using System.Collections.Generic;
using Nico.Interface;
using Nico.Player;
using UnityEngine;
namespace Games.CricketGame.Code.Player
{
    public class Player : MonoBehaviour
    {
        private PlayerInputHandler _handler;
        [SerializeField] private PlayerPhysicsData physicsData;
        #region Components

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        
        
        
        
        

        #endregion
        #region CoreComponents
        
        private readonly List<ICoreComponent> _components = new();
        
        #endregion



        private void Awake()
        {
            // Application.targetFrameRate = -1;
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _handler = transform.Find("InputHandler").GetComponent<PlayerInputHandler>();
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
    }
}