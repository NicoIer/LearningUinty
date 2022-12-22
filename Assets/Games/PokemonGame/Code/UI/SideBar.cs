using System;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Code.UI
{
    public class SideBar : MonoBehaviour
    {
        [SerializeField] private BaseControl baseControl;
        [SerializeField] private Button pokemonBtn;
        [SerializeField] private Button manualBtn;
        [SerializeField] private Button bagBtn;
        [SerializeField] private Button infoBtn;
        [SerializeField] private Button saveBtn;
        [SerializeField] private Button settingBtn;

        private void Awake()
        {
            if (baseControl is null)
            {
                baseControl = transform.parent.GetComponent<BaseControl>();
            }
            get_components();
            pokemonBtn.onClick.AddListener(baseControl.OnPokemonBtnClicked);
            manualBtn.onClick.AddListener(baseControl.OnManalBtnClicked);
            bagBtn.onClick.AddListener(baseControl.OnBagBtnClicked);
            infoBtn.onClick.AddListener(baseControl.OnInfoBtnClicked);
            saveBtn.onClick.AddListener(baseControl.OnSaveBtnClicked);
            settingBtn.onClick.AddListener(baseControl.OnSettingBtnClicked);
        }

        private void get_components()
        {
            //ToDo 当找不到时 直接添加子物体 和 button组件
            if (pokemonBtn == null)
            {
                pokemonBtn = transform.Find("pokemon").GetComponent<Button>();
                if (pokemonBtn == null)
                {
                    throw new NullReferenceException("找不到Pokemon的Button组件");
                }
            }

            if (manualBtn == null)
            {
                manualBtn = transform.Find("manual").GetComponent<Button>();
                if (manualBtn == null)
                {
                    throw new NullReferenceException("找不到manual的Button组件");
                }
            }

            if (bagBtn == null)
            {
                bagBtn = transform.Find("bag").GetComponent<Button>();
                if (bagBtn == null)
                {
                    throw new NullReferenceException("找不到bag的Button组件");
                }
            }

            if (infoBtn == null)
            {
                infoBtn = transform.Find("info").GetComponent<Button>();
                if (infoBtn == null)
                {
                    throw new NullReferenceException("找不到info的Button组件");
                }
            }

            if (saveBtn == null)
            {
                saveBtn = transform.Find("save").GetComponent<Button>();
                if (saveBtn == null)
                {
                    throw new NullReferenceException("找不到save的Button组件");
                }
            }

            if (settingBtn == null)
            {
                settingBtn = transform.Find("setting").GetComponent<Button>();
                if (settingBtn == null)
                {
                    throw new NullReferenceException("找不到setting的Button组件");
                }
            }
        }


    }
}