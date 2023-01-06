using System;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Learning
{
    public class Bar : MonoBehaviour
    {
        public Image image;
        public AsyncLearning asyncLearning;

        private void Awake()
        {
            asyncLearning.change += SetFillAmount;
        }

        public void SetFillAmount(float rate)
        {
            image.fillAmount = rate;
            print(rate);
        }
    }
}