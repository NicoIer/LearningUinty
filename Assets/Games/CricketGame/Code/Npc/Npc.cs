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
        public List<CricketData> crickets;

        private void Awake()
        {
            crickets[0].RandomInit(60);
            crickets[0].name = "敌人";
        }

        public Skill random_skill()
        {
            var idx = RandomManager.Next(0, 4);
            return crickets[0].skills[idx];
        }

        public bool HaveAvaliableCricket()
        {
            foreach (var cricketData in crickets)
            {
                if (cricketData.healthAbility > 0)
                {
                    return true;
                }
            }

            return false;
        }
        
    }
}