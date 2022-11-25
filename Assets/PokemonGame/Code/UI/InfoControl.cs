using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokemonGame.UI
{
    public class InfoControl : MonoBehaviour
    {
        [SerializeField] public PokemonSel pokemonSel;

        private void Awake()
        {
            if (pokemonSel == null)
            {
                pokemonSel = transform.Find("pokemonSel").GetComponent<PokemonSel>();
            }
        }

        public void ActivePokemonSel()
        {
            gameObject.SetActive(true);
            pokemonSel.gameObject.SetActive(true);
            print("激活破壳吗选择面板");
        }

        public void BackFromPokemonSel()
        {
            pokemonSel.gameObject.SetActive(false);
            gameObject.SetActive(false);
            UIManager.instance.BackToBasePanel();
            print("返回base面板");
        }
    }
}