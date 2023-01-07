using System;
using System.Collections.Generic;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Cricket_;
using Nico.Common;
using UnityEngine;

namespace Games.CricketGame.Npc_
{
    public class Npc : MonoBehaviour
    {
        public List<Cricket> crickets;

        private void Awake()
        {
            crickets[0].data.RandomInit(60);
            crickets[0].data.name = "敌人";
        }

        public Skill random_skill()
        {//ToDo 修复这里
            var idx = RandomManager.Next(0, 4);
            return crickets[0].data.skills[idx];
        }
    }
}