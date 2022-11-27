using System;
using UnityEngine;

namespace PokemonGame.Structs
{
    public enum Ability
    {
        Health,
        ObjectAttack,
        SpecialAttack,
        ObjectDefense,
        SpecialDefense,
        Speed
    }
    [Serializable]
    public struct AbilityValue
    {
        [SerializeField] internal uint Health;
        [SerializeField] internal uint ObjectAttack;
        [SerializeField] internal uint SpecialAttack;
        [SerializeField] internal uint ObjectDefense;
        [SerializeField] internal uint SpecialDefense;
        [SerializeField] internal uint Speed;
    }
}