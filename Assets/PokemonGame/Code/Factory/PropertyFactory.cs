using System;
using System.Collections.Generic;
using PokemonGame.Code.Structs;
using UnityEngine;

namespace PokemonGame.Code.Factory
{
    public class PropertyFactory : MonoBehaviour
    {
        [Serializable]
        public struct Storage
        {
            public PropertyEnum attack;
            public PropertyEnum defense;
            
        }

        public static PropertyFactory instance;
        public List<Storage> restrain_map;

        private void Awake()
        {
            instance = this;
        }

        private PropertyFactory()
        {
        }
    }
}