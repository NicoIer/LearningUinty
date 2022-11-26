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
        }

        pokemonCell.Clear();
        var top = transform.Find("top");
        for (var i = 0; i < top.childCount; i++)
        {
            var cell = top.GetChild(i).GetComponent<PokemonCell>();
            cell.index = (byte)i;
            pokemonCell.Add(cell);
        }


        if (backBtn == null)
        {
            backBtn = transform.Find("bottom").transform.Find("buttons").transform.Find("BackBtn")
                .GetComponent<Button>();
        }

        backBtn.onClick.AddListener(infoControl.BackFromPokemonSel);
    }


    public void OnPokemonCellClicked(Pokemon pokemon, uint index)
    {
        if (pokemon != null)
        {
            //关闭自身显示
            gameObject.SetActive(false);
            //激活Pokemon详情
            infoControl.EnterPokemonDetail(pokemon, index);
        }
        else
        {
            Debug.LogWarning("点击了没有存放宝可梦的Cell");
        }
    }

    public void HidePokemonSelect()
    {
        gameObject.SetActive(false);
    }

    public void ShowPokemonSelect()
    {
        //ToDo 在展示时 获取玩家当前携带宝可梦的情况 为PokemonCell持有的Pokemon赋值
        gameObject.SetActive(true);
    }
}