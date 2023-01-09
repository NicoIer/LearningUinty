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
            var i =0;
            foreach (var cricket in crickets)
            {
                cricket.RandomInit(30);
                cricket.name = $"敌人{i}";
            }
        }

        public Skill random_skill(CricketData data)
        {
            var idx = RandomManager.Next(0, data.skills.Count);
            return data.skills[idx];
        }

        public CricketData FirstAvailableCricket()
        {
            foreach (var cricket in crickets)
            {
                if (cricket.healthAbility > 0)
                {
                    return cricket;
                }
            }
            Debug.LogWarning("Npc没有可用的cricket啦!!!");
            return null;
        }
        public bool HaveAvailableCricket()
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