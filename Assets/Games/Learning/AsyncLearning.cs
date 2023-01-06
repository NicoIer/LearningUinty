using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Games.Learning
{
    public class AsyncLearning : MonoBehaviour
    {
        public Button button;
        public Action<float> change;
        [Range(0, 1)] public float value;

        private void Awake()
        {
            button.onClick.AddListener(_btn_clicked);
        }

        private async void _btn_clicked()
        {
            var one = value / 10;
            for (int i = 0; i < 30; i++)
            {
                await Task.Delay(100);
                _change(value - i * one);
            }
        }

        private void _change(float value)
        {
            change?.Invoke(value);
        }
    }
}