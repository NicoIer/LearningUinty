using System;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Cricket_;
using UnityEngine;

namespace Games.CricketGame.Attack
{
    public class AttackPosition : MonoBehaviour
    {
        [field: SerializeField] public Cricket cricket { get; private set; }
        public CricketData data => cricket.data;

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