using System;
using System.Collections;
using System.Collections.Generic;
using AttackGame.UI;
using UnityEngine;

namespace AttackGame.NPC
{
    public class NpcController : MonoBehaviour
    {
        //ToDo 暂时直接持有
        [SerializeField] private GameObject dialogue_bubbles;
        [SerializeField] private NpcData _npcData;
        private Npc _npc;
        private bool _active = false;

        private void Awake()
        {
            dialogue_bubbles.SetActive(false);
        }

        private void Start()
        {
            _npc = new Npc
            {
                data = _npcData
            };
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                //玩家靠近
                //ToDo 高亮自身 弹出可对话标记
                dialogue_bubbles.SetActive(true);
                _active = true;
            }
            else
            {
                return;
            }
        }

        private void Update()
        {
            if (!_active)
                return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!UIManager.instance.talkingManager.opened)
                {
                    UIManager.instance.OpenTalkingPanel(_npc.data.taling_data);
                }
                else
                {
                    UIManager.instance.CloseTalkingPanel();
                }
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                dialogue_bubbles.SetActive(false);
                if (UIManager.instance.talkingManager.opened)
                    UIManager.instance.CloseTalkingPanel();
                _active = false;
            }
        }
    }
}