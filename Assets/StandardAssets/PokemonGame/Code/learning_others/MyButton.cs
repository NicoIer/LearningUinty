namespace PokemonGame.Code.leaning_others
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;

    /// <summary>
    /// 我的自定义Button，继承 Button
    /// </summary>
    public class MyButton : Button
    {
        // 构造函数
        protected MyButton()
        {
            _myOnDoubleClick = new ButtonClickedEvent();
            _myOnLongPress = new ButtonClickedEvent();
        }

        // 长按
        private ButtonClickedEvent _myOnLongPress;

        public ButtonClickedEvent OnLongPress
        {
            get => _myOnLongPress;
            set => _myOnLongPress = value;
        }

        // 双击
        private ButtonClickedEvent _myOnDoubleClick;

        public ButtonClickedEvent OnDoubleClick
        {
            get => _myOnDoubleClick;
            set => _myOnDoubleClick = value;
        }

        // 长按需要的变量参数
        public bool startPress;
        private float _myCurPointDownTime;
        private const float _myLongPressTime = 0.6f;
        public bool _myLongPressTrigger;


        private void Update()
        {
            CheckIsLongPress();
        }

        #region 长按

        /// <summary>
        /// 处理长按
        /// </summary>
        private void CheckIsLongPress()
        {
            if (startPress && !_myLongPressTrigger)
            {
                //开始按下 且 未进入长按状态
                if (Time.time > _myCurPointDownTime + _myLongPressTime)
                {
                    //通过时间 计算 是否进入长按状态
                    _myLongPressTrigger = true;
                    startPress = false;
                    OnLongPress?.Invoke(); //调用委托
                }
            }
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            //每次按下都会调用
            // 按下刷新當前時間
            base.OnPointerDown(eventData);
            _myCurPointDownTime = Time.time;
            startPress = true;
            _myLongPressTrigger = false;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            // 松开
            base.OnPointerUp(eventData);
            startPress = false;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            // 指針移出，結束開始長按，計時長按標志
            base.OnPointerExit(eventData);
            startPress = false;
        }

        #endregion

        #region 双击（单击）

        public override void OnPointerClick(PointerEventData eventData)
        {
            //(避免已經點擊進入長按后，擡起的情況)
            if (!_myLongPressTrigger)
            {
                // 正常單擊 
                if (eventData.clickCount == 2)
                {
                    if (_myOnDoubleClick != null)
                    {
                        _myOnDoubleClick.Invoke();
                    }
                } // 雙擊
                else if (eventData.clickCount == 1)
                {
                    onClick.Invoke();
                }
            }
        }

        #endregion
    }
}