using System;
using System.Collections;
using System.Collections.Generic;
using PokemonGame;
using PokemonGame.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PokemonSel : MonoBehaviour
{
    [SerializeField] private InfoControl infoControl;
    [SerializeField] private List<PokemonCell> pokemonCell;
    [SerializeField] private Button backBtn; 
    private void Awake()
    {
        if (infoControl == null)
        {
            infoControl = transform.parent.GetComponent<InfoControl>();
        }
        if (pokemonCell == null)
        {
            pokemonCell = new List<PokemonCell>();
            var top = transform.Find("top");
            for (int i = 0; i < top.childCount; i++)
            {
                pokemonCell.Add(top.GetChild(i).GetComponent<PokemonCell>());
            }
        }
        else if (pokemonCell.Count != 6)
        {
            pokemonCell.Clear();
            var top = transform.Find("top");
            for (int i = 0; i < top.childCount; i++)
            {
                pokemonCell.Add(top.GetChild(i).GetComponent<PokemonCell>());
            }
        }

        if (backBtn == null)
        {
            backBtn = transform.Find("bottom").transform.Find("buttons").transform.Find("BackBtn")
                .GetComponent<Button>();
        }
        backBtn.onClick.AddListener(infoControl.BackFromPokemonSel);
    }

    public static void OnPokemonCellClicked(Pokemon pokemon)
    {
        if (pokemon != null)
        {//ToDo 进行UI处理
            print("选中了"+pokemon.info.id);
        }
        else
        {
            print("无pokemon可查看");
        }
    }
}