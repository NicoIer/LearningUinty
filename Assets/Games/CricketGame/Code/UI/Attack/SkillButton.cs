using System;
using Games.CricketGame.Cricket_;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.UI
{
    public class SkillButton : MonoBehaviour
    {
        public Button button;
        public Text nameText;
        public Text effectText;
        public Image propertyImage;
        public Text ppText;
        public Action onClicked;
        private bool _banded;
        private Skill _skill;

        private void Awake()
        {
            if (nameText == null)
            {
                nameText = transform.Find("nameText").GetComponent<Text>();
            }

            if (button == null)
            {
                button = GetComponent<Button>();
            }
            button.onClick.AddListener(_clicked);
        }

        public void Band(Skill skill)
        {
            _banded = true;
            _skill = skill;
            //ToDo 这里只做测试
            print(skill.meta.name);
            nameText.text = skill.meta.name;
            ppText.text = $"{skill.cur_times}/{skill.use_times}";
            // propertyImage.sprite = sprite;
            // effectText.text = effect;
        }

        private void _clicked()
        {
            onClicked.Invoke();
        }
        
    }
}