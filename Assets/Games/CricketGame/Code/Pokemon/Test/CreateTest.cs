using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon.Test
{
    
    public class CreateTest : MonoBehaviour
    {
        private void Awake()
        {
            var pokemon = PokemonDataMeta.Find(PokemonEnum.史莱姆);
            print(pokemon.property);
        }
    }
    
}

