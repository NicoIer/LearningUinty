using System;
using UnityEngine;

namespace AttackGame
{
    /// <summary>
    /// 掉落在地上的物品 可以被捡起
    /// </summary>
    public class DroppedItem : MonoBehaviour
    {
        public InspectorItem inspectorItem;
        private Item _item;

        private void Awake()
        {
            _item = new Item(inspectorItem.data, inspectorItem.info.num);
        }

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
            //ToDo 应该交给Player做处理
            UIManager.instance.packageManager.AddItem(_item);
        }
    }
}