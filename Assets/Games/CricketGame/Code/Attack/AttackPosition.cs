using System;
using Games.CricketGame.Code.Cricket_;
using UnityEngine;

namespace Games.CricketGame.Code.Attack
{
    public class AttackPosition : MonoBehaviour
    {
        public Cricket cricket;
        
        private void Awake()
        {
            cricket = transform.GetChild(0).GetComponent<Cricket>();
        }

        public void ResetData(CricketData data)
        {
            cricket.ReSetData(data);
        }
    }
}