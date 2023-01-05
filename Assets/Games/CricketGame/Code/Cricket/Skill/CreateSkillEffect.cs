using System;
using Games.CricketGame.Manager.Code.Manager;
using UnityEngine;

namespace Games.CricketGame.Manager.Code.Pokemon.Skill
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