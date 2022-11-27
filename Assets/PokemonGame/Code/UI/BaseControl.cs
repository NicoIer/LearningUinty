using System;
using UnityEngine;

namespace PokemonGame.Code.UI
{
    public class BaseControl : MonoBehaviour
    {
        [SerializeField] private SideBar sideBar;
        private void Awake()
        {
            if (sideBar == null)
            {
                sideBar = transform.Find("SideBar").GetComponent<SideBar>();
                if (sideBar == null)
                {
                    throw new NullReferenceException("找不到SideBar");
                }
            }
        }
        
    
        public void ActiveSideBar(bool active)
        {
            sideBar.gameObject.SetActive(active);
        }
        #region Btn Event
    
        public void OnPokemonBtnClicked()
        {
            gameObject.SetActive(false);
            UIManager.instance.OnPokemonBtnClicked();
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
