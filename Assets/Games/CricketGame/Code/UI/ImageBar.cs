using System;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.Manager.Code.UI
{
    public class ImageBar : MonoBehaviour
    {
        private Image _fillImage;

        public float fillAmount
        {
            get => _fillImage.fillAmount;
            set => _fillImage.fillAmount = value;
        }
        

        private void Awake()
        {
            if (_fillImage == null)
            {
                _fillImage = transform.GetChild(0).GetComponent<Image>();
            }
        }


    }
}