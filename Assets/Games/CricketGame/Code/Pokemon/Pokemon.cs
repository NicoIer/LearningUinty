using System;
using System.Collections.Generic;
using Nico.Interface;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon
{
    public class Pokemon : MonoBehaviour
    {
        [field: SerializeField] public PokemonData data { get; private set; }
        [field: SerializeField] public PokemonInputHandler handler { get; private set; }
        private readonly List<ICoreComponent> _components = new();


        private void Awake()
        {
            var controller = new Controller(this);
            _components.Add(controller);
        }

        private void Start()
        {
            foreach (var coreComponent in _components)
            {
                coreComponent.Start();
            }
        }

        private void Update()
        {
            foreach (var coreComponent in _components)
            {
                coreComponent.Update();
            }
        }

        private void FixedUpdate()
        {
            foreach (var coreComponent in _components)
            {
                coreComponent.FixedUpdate();
            }
        }
    }
}