using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PokemonGame.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;
        [SerializeField] private InfoControl infoControl;
        [SerializeField] private BaseControl baseControl;
        [SerializeField] private AttackControl attackControl;
        private bool _baseOpened;
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