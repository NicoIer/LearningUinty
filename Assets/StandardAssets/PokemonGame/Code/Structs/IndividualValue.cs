using System;
using UnityEngine;

namespace PokemonGame.Structs
{
    [Serializable]
    public struct IndividualValue
    {
        [SerializeField] internal ushort Health;
        [SerializeField] internal ushort ObjectAttack;
        [SerializeField] internal ushort SpecialAttack;
        [SerializeField] internal ushort ObjectDefense;
        [SerializeField] internal ushort SpecialDefense;
        [SerializeField] internal ushort Speed;
    }
}