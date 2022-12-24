using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AttackGame.NPC
{
    public class NpcController : MonoBehaviour
    {
        //ToDo 暂时直接持有
        [SerializeField] private GameObject dialogue_bubbles;
        private Npc _npc;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {//玩家靠近
                //ToDo 高亮自身 弹出可对话标记
                dialogue_bubbles.SetActive(true);
            }
            else
            {
                return;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                dialogue_bubbles.SetActive(false);
            }
        }
    }
}