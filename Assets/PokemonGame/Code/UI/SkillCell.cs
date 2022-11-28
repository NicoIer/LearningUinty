using System;
using PokemonGame.Code.Factory;
using PokemonGame.Code.Structs;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Code.UI
{
    public class SkillCell : MonoBehaviour
    {
        private Skill _skill;
        public Image background;
        public Text nameText;
        public Text powerText;
        public Text ppTitle;
        public Text ppText;
        public Text propertyText;
        public Image propertyImage;

        private void Awake()
        {
            init_components();
        }

        private void init_components()
        {
            if (background == null)
            {
                background = GetComponent<Image>();
            }

            if (nameText == null)
            {
                nameText = transform.Find("name").GetComponent<Text>();
            }

            if (powerText == null)
            {
                powerText = transform.Find("power").GetComponent<Text>();
            }

            if (propertyImage == null)
            {
                propertyImage = transform.Find("property").GetComponent<Image>();
            }

            if (propertyText == null)
            {
                propertyText = propertyImage.transform.Find("text").GetComponent<Text>();
            }

            if (ppTitle == null)
            {
                ppTitle = transform.Find("pp").GetComponent<Text>();
            }

            if (ppText == null)
            {
                ppText = ppTitle.transform.Find("text").GetComponent<Text>();
            }
        }

        public void update_cell(Skill skill)
        {
            _skill = skill;
            if (_skill != null)
            {
                if (_skill.skillEnum != SkillEnum.None)
                {
                    print("更新SkillCell");
                    enable_components();
                    //pp
                    ppText.text = $"{_skill.cur_pp}/{_skill.pp}";
                    //技能名称
                    nameText.text = _skill.name;
                    //技能属性
                    propertyImage.sprite = _skill.property.icon;
                    propertyText.text = _skill.property.name;
                    //技能类型
                    switch (_skill.typeEnum)
                    {
                        case SkillTypeEnum.物理:
                            background.color = Color.gray;
                            break;
                        case SkillTypeEnum.变化:
                            background.color = Color.blue;

                            break;
                        case SkillTypeEnum.特殊:
                            background.color = Color.green;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    //power
                    if (_skill.typeEnum != SkillTypeEnum.变化)
                    {
                        powerText.text = _skill.power.ToString();
                    }
                }
                else
                {
                    disable_components();
                }
            }
            else
            {
                disable_components();
            }
        }

        private void disable_components()
        {
            init_components();
            ppTitle.enabled = false;
            ppText.enabled = false;
            nameText.enabled = false;
            propertyImage.enabled = false;
            propertyText.enabled = false;
            powerText.enabled = false;
            background.color = Color.gray;
        }

        private void enable_components()
        {
            init_components();
            ppTitle.enabled = true;
            ppText.enabled = true;
            nameText.enabled = true;
            propertyImage.enabled = true;
            propertyText.enabled = true;
            powerText.enabled = true;
        }
    }
}