using PackageGame.UI;
using UnityEngine;

namespace PackageGame.NPC
{
    public class NpcController : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueBubbles;

        [SerializeField] private NpcData _data;
        private bool _active;

        #region Unity CallBack

        private void Awake()
        {
            if (dialogueBubbles == null)
                dialogueBubbles = transform.GetChild(0).gameObject;
            dialogueBubbles.SetActive(false);
        }

        private void Update()
        {
            if (!_active)
                return;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!UIManager.instance.talkingManager.opened)
                {
                    var talkingData = _data.talkingData;
                    if (talkingData != null)
                        UIManager.instance.OpenTalkingPanel(talkingData);
                }
                else
                {
                    UIManager.instance.CloseTalkingPanel();
                }
            }
        }

        #endregion

        #region Unity Envent

        private void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("Player"))
            {
                //玩家靠近
                //ToDo 高亮自身 弹出可对话标记
                dialogueBubbles.SetActive(true);
                _active = true;
            }
            else
            {
                return;
            }
        }


        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                dialogueBubbles.SetActive(false);
                if (UIManager.instance.talkingManager.opened)
                    UIManager.instance.CloseTalkingPanel();
                _active = false;
            }
        }

        #endregion
    }
}