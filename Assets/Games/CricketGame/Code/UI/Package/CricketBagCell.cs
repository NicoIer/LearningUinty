using System;
using Games.CricketGame.Cricket_;
using UnityEngine.UI;

namespace Games.CricketGame.UI
{
    public class CricketBagCell : AbsCricketUI
    {
        //背包中的cricket格子

        #region UI

        public Image cricketIcon;
        public Image itemIcon;
        public Text nameText;
        public Text hpText;
        public ImageBar hpBar;
        public Image sexIcon;
        public Image stateIcon;
        public Text levelText;
        public Button button;

        #endregion

        public int idx;
        public Action<int> onClickedAction;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(_on_clicked);
        }

        private void _on_clicked()
        {
            onClickedAction.Invoke(idx);
        }

        public override void Connect(CricketData cricket)
        {
            base.Connect(cricket);
            Activate(true);
            _level_change(_data.level, _data.level);
            _name_change(_data.name, _data.name);
            _health_change(_data.healthAbility, _data.defaultHealth);
        }

        public void Clear()
        {
            DisConnect();
            Activate(false);
        }

        private void Activate(bool hide)
        {
            levelText.gameObject.SetActive(hide);
            sexIcon.gameObject.SetActive(hide);
            hpText.gameObject.SetActive(hide);
            cricketIcon.gameObject.SetActive(hide);
            itemIcon.gameObject.SetActive(hide);
            nameText.gameObject.SetActive(hide);
            hpText.gameObject.SetActive(hide);
            hpBar.gameObject.SetActive(hide);
            sexIcon.gameObject.SetActive(hide);
            stateIcon.gameObject.SetActive(hide);
            levelText.gameObject.SetActive(hide);
        }


        protected override void _exp_change(float attained, float total)
        {
            throw new NotImplementedException();
        }

        protected override void _level_change(int pre, int now)
        {
            levelText.text = now.ToString();
        }

        protected override void _name_change(string pre, string now)
        {
            nameText.text = now;
        }

        protected override void _meta_change(CricketData data)
        {
            throw new NotImplementedException();
        }

        protected override void _health_change(float cur, float max)
        {
            hpBar.fillAmount = cur / max;
            hpText.text = $"{(int)cur}/{(int)max}";
        }
    }
}