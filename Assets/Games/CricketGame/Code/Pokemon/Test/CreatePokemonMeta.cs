using System.Collections.Generic;
using Games.CricketGame.Code.Pokemon.Skill;
using Games.CricketGame.Code.Pokemon.Test.ScriptableObject;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon.Test
{

    public class CreatePokemonMeta : MonoBehaviour
    {
        public List<PokemonMetaInspector> pokemons = new();
        
        
        private void Awake()
        {
            foreach (var pokemon in pokemons)
            {
                pokemon.Initialize();
            }
            PokemonDataMeta.Save();
            SkillMeta.Save();
            
        }
        
        
    }
}