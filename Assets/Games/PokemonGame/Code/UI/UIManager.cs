using System;
using System.Collections.Generic;
using PokemonGame.Code.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PokemonGame.Code.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        [SerializeField] private InfoControl infoControl;
        [SerializeField] private BaseControl baseControl;
        [SerializeField] private AttackControl attackControl;
        private bool _baseOpened;
        [SerializeField] private bool listening_event = false;

        private void Awake()
        {
            instance = this;
            if (infoControl == null)
            {
                infoControl = transform.Find("info").GetComponent<InfoControl>();
            }

            if (baseControl is null)
            {
                baseControl = transform.Find("base").GetComponent<BaseControl>();
            }

            if (attackControl is null)
            {
                attackControl = transform.Find("attack").GetComponent<AttackControl>();
            }

            infoControl.gameObject.SetActive(false);
            baseControl.gameObject.SetActive(false);
            attackControl.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C) && !_baseOpened)
            {
                _baseOpened = true;
                baseControl.gameObject.SetActive(_baseOpened);
                baseControl.ActiveSideBar(_baseOpened);
            }
            else if (Input.GetKeyDown(KeyCode.C) && _baseOpened)
            {
                _baseOpened = false;
                baseControl.gameObject.SetActive(_baseOpened);
                baseControl.ActiveSideBar(_baseOpened);
            }

            if (listening_event)
            {
                //在这里检测 鼠标事件 并 分发到各个脚本组件上去
                if (Input.GetMouseButtonDown(0))
                {
                    //点击后
                    var obj = GetFirstPickGameObject(Input.mousePosition);
                    if (obj is not null)
                    {
                        print($"UIManager - 鼠标左键点击{obj.name}");
                    }
                }

                else if (Input.GetMouseButtonUp(0))
                {
                    print("UIManager - 鼠标左键松开");
                }
            }
        }

        /// <summary>
        /// 点击屏幕坐标
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private GameObject GetFirstPickGameObject(Vector2 position)
        {
            EventSystem eventSystem = EventSystem.current;
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = position;
            //射线检测ui
            List<RaycastResult> uiRaycastResultCache = new List<RaycastResult>();
            eventSystem.RaycastAll(pointerEventData, uiRaycastResultCache);
            if (uiRaycastResultCache.Count > 0)
                return uiRaycastResultCache[0].gameObject;
            return null;
        }

        #region InfoPanel-Event

        public void BackToBasePanel()
        {
            baseControl.gameObject.SetActive(true);
        }

        #endregion

        #region BasePanel-Event

        public void OnPokemonBtnClicked()
        {
            infoControl.ActivePokemonSel();
        }

        public void OnManalBtnClicked()
        {
            print("clicked:" + System.Reflection.MethodBase.GetCurrentMethod()?.Name);
        }

        public void OnBagBtnClicked()
        {
            print("clicked:" + System.Reflection.MethodBase.GetCurrentMethod()?.Name);
        }

        public void OnInfoBtnClicked()
        {
            print("clicked:" + System.Reflection.MethodBase.GetCurrentMethod()?.Name);
        }

        public void OnSaveBtnClicked()
        {
            print("clicked:" + System.Reflection.MethodBase.GetCurrentMethod()?.Name);
        }

        public void OnSettingBtnClicked()
        {
            print("clicked:" + System.Reflection.MethodBase.GetCurrentMethod()?.Name);
        }

        #endregion
    }
}