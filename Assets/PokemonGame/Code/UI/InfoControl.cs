using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.UI
{
    public class InfoControl : MonoBehaviour
    {
        [SerializeField] public PokemonSel pokemonSel;
        [SerializeField] public PokemonDetail pokemonDetail;

        private void Awake()
        {
            if (pokemonSel == null)
            {
                pokemonSel = transform.Find("pokemonSel").GetComponent<PokemonSel>();
            }

            if (pokemonDetail == null)
            {
                pokemonDetail = transform.Find("pokemonDetail").GetComponent<PokemonDetail>();
            }
        }

        public void ActivePokemonSel()
        {
            gameObject.SetActive(true);
            pokemonSel.ShowPokemonSelect();
        }

        public void BackFromPokemonSel()
        {
            pokemonSel.HidePokemonSelect();
            gameObject.SetActive(false);
            UIManager.instance.BackToBasePanel();
        }

        public void BackFromPokemonDetail()
        {
            //取消详细面板的显示
            gameObject.SetActive(true);
            pokemonDetail.HidePokemonDetail();
            //激活选择面板
            ActivePokemonSel();
        }

        public void ExitFromPokemonDetail()
        {
            pokemonDetail.HidePokemonDetail();
            gameObject.SetActive(false);
            UIManager.instance.BackToBasePanel();
        }

        public void EnterPokemonDetail(Pokemon pokemon, uint index)
        {
            gameObject.SetActive(true);
            pokemonDetail.update_pokemon(pokemon, index);
            pokemonDetail.ShowPokemonDetail();
        }
    }
}