using System;
using PokemonGame.Code.Structs;
using UnityEngine;

namespace PokemonGame
{
    public enum StateEnum
    {
        无,
        中毒,
        睡眠,
        剧毒,
        瞌睡,
        麻痹,
        烧伤,
        冰冻,
        冻伤
    }
    
    public class State
    {
        public static State find_state(StateEnum stateEnum)
        {//ToDo 将icon也添加到这里来 最好是做成序列化的JSON文件
            return new State(stateEnum.ToString(), stateEnum);
        }

        State(string name, StateEnum stateEnum)
        {
            this.name = name;
            this.stateEnum = stateEnum;
        }

        public string name;
        public StateEnum stateEnum;
        public Sprite icon;

        public void apply_effect(ref PokemonBase pokemonBase)
        {//ToDo 完善这里
            switch (stateEnum)
            {
                case StateEnum.无:
                    break;
                case StateEnum.中毒:
                    break;
                case StateEnum.睡眠:
                    break;
                case StateEnum.剧毒:
                    break;
                case StateEnum.瞌睡:
                    break;
                case StateEnum.麻痹:
                    break;
                case StateEnum.烧伤:
                    break;
                case StateEnum.冰冻:
                    break;
                case StateEnum.冻伤:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}