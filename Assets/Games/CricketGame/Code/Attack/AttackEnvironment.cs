using System;
using System.Collections.Generic;
using UnityEngine;

namespace Games.CricketGame.Code.Attack
{
    public class AttackEnvironment : MonoBehaviour
    {
        private List<Transform> _objects;

        private void Awake()
        {
            _objects = new();
            for (int i = 0; i < transform.childCount; i++)
            {
                _objects.Add(transform.GetChild(i));
            }
        }

        public void UpdateRotation(Quaternion quaternion)
        {
            foreach (var o in _objects)
            {
                o.localRotation = quaternion;
            }
        }
    }
}