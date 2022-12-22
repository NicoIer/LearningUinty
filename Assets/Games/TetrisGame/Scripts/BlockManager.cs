using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    /// <summary>
    /// BlockManager 用于管理生成/销毁方块
    /// </summary>
    public class BlockManager : MonoBehaviour
    {
        public GameObject block;
        private Block current_block;

        private void Start()
        {
            create_block();
        }

        private void Update()
        {
            if (current_block.over)
            {
                create_block();
            }
        }

        private void create_block()
        {
            current_block = Instantiate(block, transform.position, Quaternion.identity, transform)
                .GetComponent<Block>();
        }

        public void destroy_block()
        {
        }
    }
}