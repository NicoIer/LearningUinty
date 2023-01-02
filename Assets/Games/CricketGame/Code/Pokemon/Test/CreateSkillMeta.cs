using System;
using System.Collections.Generic;
using Games.CricketGame.Code.Pokemon.Skill;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon.Test
{
    public class CreateSkillMeta : MonoBehaviour
    {
        public List<SkillMeta> metas = new();

        private void Awake()
        {
            foreach (var meta in metas)
            {
                SkillMeta.Add(meta);
            }
            SkillMeta.Save();
        }
    }
}