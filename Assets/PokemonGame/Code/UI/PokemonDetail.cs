using System;
using System.Collections;
using System.Collections.Generic;
using PokemonGame;
using PokemonGame.UI;
using UnityEngine;

public class PokemonDetail : MonoBehaviour
{
    [SerializeField] public InfoControl infoControl;
    [SerializeField] public PokemonDetailLeft left;
    [SerializeField] public PokemonDetailRight right;

    private void Awake()
    {
        if (infoControl == null)
        {
            infoControl = transform.parent.GetComponent<InfoControl>();
        }

        if (left == null)
        {
            left = transform.Find("left").Find("pokemon-detail").GetComponent<PokemonDetailLeft>();
        }

        if (right == null)
        {
            right = transform.Find("right").Find("pokemon-detail").GetComponent<PokemonDetailRight>();
        }
    }

    public void ShowPokemonDetail()
    {
        gameObject.SetActive(true);
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
    }
    

    public void HidePokemonDetail()
    {
        gameObject.SetActive(false);
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);
    }

    public void update_pokemon(Pokemon pokemon, uint index)
    {
        left.set_pokemon(pokemon);
        right.set_pokemon(pokemon, index);
    }

    public void OnBackButtonClicked()
    {
        right.gameObject.SetActive(false);
        left.gameObject.SetActive(true);
        gameObject.SetActive(false);
        infoControl.BackFromPokemonDetail();
    }

    public void OnExitBtnClicked()
    {
        infoControl.ExitFromPokemonDetail();
    }
}