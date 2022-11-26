using System;
using System.Collections;
using System.Collections.Generic;
using PokemonGame;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.UI
{
    public class PokemonDetailLeft : MonoBehaviour
    {
        //用于显示的_pokemon
        private Pokemon _pokemon;
        [SerializeField] private PokemonDetail _pokemonDetail;

        [Header("Info")] [SerializeField] private Text noText;
        [SerializeField] private Text nameText;
        [SerializeField] private GameObject propertyObj;
        private GameObject _firstProperty;
        private GameObject _secondProperty;
        [SerializeField] private Transform propertiesTransform;
        [SerializeField] private Text trainerText;
        [SerializeField] private Text idText;
        [SerializeField] private Text expNowText;
        [SerializeField] private Text expNowNeedText;

        [Header("Peculiarity")] [SerializeField]
        private Text peculiarityNameText;

        [SerializeField] private Text peculiarityDescText;
        [Header("Ability")] [SerializeField] private Text hpText;
        [SerializeField] private Image hpMaskImage;
        [SerializeField] private Text objectAttackText;
        [SerializeField] private Text objectDefenseText;
        [SerializeField] private Text specialAttackText;
        [SerializeField] private Text specialDefenseText;
        [SerializeField] private Text speedText;

        private void Awake()
        {
            if (_pokemonDetail == null)
            {
                _pokemonDetail = transform.parent.parent.GetComponent<PokemonDetail>();
            }

            var info = transform.Find("info");
            var peculiarity = transform.Find("peculiarity");
            var ability = transform.Find("ability");
            //Info
            if (noText is null)
            {
                noText = info.Find("No").Find("no").GetComponent<Text>();
            }

            if (nameText is null)
            {
                nameText = info.Find("Name").Find("name").GetComponent<Text>();
            }

            if (propertiesTransform == null)
            {
                propertiesTransform = info.Find("Property").Find("properties");
            }

            if (propertyObj == null)
            {
                propertyObj = propertiesTransform.Find("property").gameObject;
            }

            if (trainerText is null)
            {
                trainerText = info.Find("Trainer").Find("trainer").GetComponent<Text>();
            }

            if (idText is null)
            {
                idText = info.Find("ID").Find("id").GetComponent<Text>();
            }

            expNowText ??= info.Find("Empirical-now").Find("empirical").GetComponent<Text>();

            expNowNeedText ??= info.Find("Empirical-need").Find("empirical").GetComponent<Text>();

            //peculiarity
            peculiarityNameText ??= peculiarity.Find("name-background").Find("name").GetComponent<Text>();

            peculiarityDescText ??= peculiarity.Find("des-background").Find("des").GetComponent<Text>();

            //ability
            hpText ??= ability.Find("HP").Find("info").GetComponent<Text>();
            hpMaskImage ??= ability.Find("HP-Bar").Find("mask").GetComponent<Image>();
            objectAttackText ??= ability.Find("ObjectAttack").Find("info").GetComponent<Text>();
            objectDefenseText ??= ability.Find("ObjectDefense").Find("info").GetComponent<Text>();
            specialAttackText ??= ability.Find("SpecialAttack").Find("info").GetComponent<Text>();
            specialDefenseText ??= ability.Find("SpecialDefense").Find("info").GetComponent<Text>();
            speedText ??= ability.Find("Speed").Find("info").GetComponent<Text>();
        }

        private void OnEnable()
        {
            update_ui();
        }

        private void OnDisable()
        {
            update_ui();
        }
        
        public void update_ui()
        {
            if (_pokemon != null)
            {
                //图鉴ID
                noText.text = _pokemon.info.id.ToString();
                //名称
                nameText.text = _pokemon.info.otherName;
                //属性
                propertyObj.SetActive(false);
                if (_pokemon.info.firstProperty != Property.None)
                {
                    if (_firstProperty == null)
                    {
                        _firstProperty = Instantiate(propertyObj, propertiesTransform);
                    }

                    _firstProperty.SetActive(true);
                    _firstProperty.GetComponent<Text>().text = _pokemon.info.firstProperty.ToString();
                }
                else
                {
                    _firstProperty.SetActive(false);
                    Debug.LogWarning("Pokemon没有第一属性!!!");
                }

                if (_pokemon.info.secondProperty != Property.None)
                {
                    if (_secondProperty == null)
                    {
                        _secondProperty = Instantiate(propertyObj, propertiesTransform);
                    }

                    _secondProperty.SetActive(true);
                    _secondProperty.GetComponent<Text>().text = _pokemon.info.secondProperty.ToString();
                }
                else
                {
                    if (_secondProperty != null)
                    {
                        _secondProperty.SetActive(false);
                    }
                }
                //训练家
                //ID
                //exp Now
                //exp Need

                //特性名
                //特效效果描述

                //hp文字
                hpText.text = $"{_pokemon.info.currentHealth}/{_pokemon.abilityValue.Health}";
                //hp-mask
                hpMaskImage.fillAmount = (float)_pokemon.info.currentHealth / _pokemon.abilityValue.Health;
                //Obj Attack
                objectAttackText.text = _pokemon.abilityValue.ObjectAttack.ToString();
                //Obj Defense
                objectDefenseText.text = _pokemon.abilityValue.ObjectDefense.ToString();
                //Spe Attack
                specialAttackText.text = _pokemon.abilityValue.SpecialAttack.ToString();
                //Spe Defense
                specialDefenseText.text = _pokemon.abilityValue.SpecialDefense.ToString();
                //Speed
                speedText.text = _pokemon.abilityValue.Speed.ToString();
            }
            else
            {
                Debug.LogWarning("Pokemon 未在当前Detail设置引用");
            }
        }

        public void set_pokemon(Pokemon pokemon)
        {
            this._pokemon = pokemon;
        }
    }
}