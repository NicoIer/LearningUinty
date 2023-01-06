using System;
using System.Collections.Generic;
using Nico.Interface;
using UnityEngine;

namespace Games.CricketGame.Code.Cricket_
{
    public class Cricket : MonoBehaviour
    {
        [field: SerializeField]public CricketData data { get; private set; }
        [field: SerializeField] public InputHandler handler { get; private set; }
        private readonly List<ICoreComponent> _components = new();


        private void Awake()
        {
            handler = transform.GetChild(0).GetComponent<InputHandler>();
            var controller = new Controller(this);
            // data = PokemonData.random_init(data.meta.pokemonEnum);
            data.UpdateDefault();
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