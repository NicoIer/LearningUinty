using System;
using PackageGame.UI;
using UnityEngine;

namespace PackageGame._Item
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
            _item = new Item(inspectorItem.data, inspectorItem.info.num,inspectorItem.info.sprite);
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                PickedByPlayer();
            }
        }

        private void PickedByPlayer()
        {
            Destroy(gameObject);
            if (UIManager.instance.packageManager.HaveCapacity(_item.uid))
            {
                UIManager.instance.packageManager.AddItem(_item);
            }
            else
            {
                print("背包没有空间存放物品了");
            }
        }
    }
}