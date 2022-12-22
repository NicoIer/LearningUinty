

namespace PokemonGame.Code.UI
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;
    public class NicoButton : Button
    {
        private bool _isPressed;
        private float _delayTime;
        private float _pressedTime;

        public bool longPressed;

        protected NicoButton()
        {
            
        }

        protected override void Awake()
        {
            base.Awake();
            _delayTime =  Time.deltaTime;
        }

        private void Update()
        {
            if (_isPressed && Time.time - _delayTime > _pressedTime)
            {
                longPressed = true;
            }
            else
            {
                longPressed = false;
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            print("pointer down");
            base.OnPointerDown(eventData);
            _isPressed = true;
            _pressedTime = Time.time;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            print("pointer up");
            base.OnPointerUp(eventData);
            _isPressed = false;
        }
    }
}