using System;
using PokemonGame.Code.Structs;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Games.CricketGame.UI
{
    public class PropertyIcon : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Text text;


        public void _update(PropertyEnum propertyEnum)
        {
            //ToDo 完成属性图标的获取模块
            text.text = propertyEnum.ToString();
        }
    }
}