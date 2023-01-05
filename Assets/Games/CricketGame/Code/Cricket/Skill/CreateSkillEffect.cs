using System;
using Games.CricketGame.Code.Pokemon.Enum;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon.Skill
{
    public class CreateSkillEffect : MonoBehaviour
    {
        private void Awake()
        {
            var map = Skill.GetEffectMap();
            foreach ((SkillEnum skillEnum, Type type) in map)
            {
                Debug.Log($"{skillEnum}--{type}");
            }
        }
    }
}