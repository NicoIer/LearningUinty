using System;
using UnityEngine;

namespace AttackGame
{
    /// <summary>
    /// 掉落在地上的物品 可以被捡起
    /// </summary>
    public class DroppedItem : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                PickedByPlayer();    
            }
        }

        private void PickedByPlayer()
        {
            Destroy(gameObject);
        }
    }
}