using System;
using System.Collections;
using System.Collections.Generic;
using PokemonGame;
using PokemonGame.Code.Structs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.Code.UI
{
    public class PokemonDetailLeft : MonoBehaviour
    {
        //用于显示的_pokemon
        [SerializeField] private Pokemon _pokemon;
        private PokemonDetail _pokemonDetail;

        private Text _noText;
        private Text _nameText;
        private GameObject _propertyObj;
        private GameObject _firstProperty;
        private GameObject _secondProperty;
        private Transform _propertiesTransform;
        private Text _trainerText;
        private Text _idText;
        private Text _expNowText;
        private Text _expNowNeedText;


        private Text _peculiarityNameText;

        private Text _peculiarityDescText;
        private Text _hpText;
        private Image _hpMaskImage;
        private Text _objectAttackText;
        private Text _objectDefenseText;
        private Text _specialAttackText;
        private Text _specialDefenseText;
        private Text _speedText;

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
            _noText ??= info.Find("No").Find("no").GetComponent<Text>();

            _nameText ??= info.Find("Name").Find("name").GetComponent<Text>();

            if (_propertiesTransform == null)
            {
                _propertiesTransform = info.Find("Property").Find("properties");
            }

            if (_propertyObj == null)
            {
                _propertyObj = _propertiesTransform.Find("property").gameObject;
            }

            if (_trainerText is null)
            {
                _trainerText = info.Find("Trainer").Find("trainer").GetComponent<Text>();
            }

            if (_idText is null)
            {
                _idText = info.Find("ID").Find("id").GetComponent<Text>();
            }

            _expNowText ??= info.Find("Empirical-now").Find("empirical").GetComponent<Text>();

            _expNowNeedText ??= info.Find("Empirical-need").Find("empirical").GetComponent<Text>();

            //peculiarity
            _peculiarityNameText ??= peculiarity.Find("name-background").Find("name").GetComponent<Text>();

            _peculiarityDescText ??= peculiarity.Find("des-background").Find("des").GetComponent<Text>();

            //ability
            _hpText ??= ability.Find("HP").Find("info").GetComponent<Text>();
            _hpMaskImage ??= ability.Find("HP-Bar").Find("mask").GetComponent<Image>();
            _objectAttackText ??= ability.Find("ObjectAttack").Find("info").GetComponent<Text>();
            _objectDefenseText ??= ability.Find("ObjectDefense").Find("info").GetComponent<Text>();
            _specialAttackText ??= ability.Find("SpecialAttack").Find("info").GetComponent<Text>();
            _specialDefenseText ??= ability.Find("SpecialDefense").Find("info").GetComponent<Text>();
            _speedText ??= ability.Find("Speed").Find("info").GetComponent<Text>();
        }

        private void OnEnable()
        {
            update_ui();
        }

        private void update_ui()
        {
            if (_pokemon != null)
            {
                //图鉴ID
                _noText.text = _pokemon.info.id.ToString();
                //名称
                _nameText.text = _pokemon.info.otherName;
                //属性
                _propertyObj.SetActive(false);
                if (_firstProperty == null)
                {
                    _firstProperty = Instantiate(_propertyObj, _propertiesTransform);
                }

                if (_secondProperty == null)
                {
                    _secondProperty = Instantiate(_propertyObj, _propertiesTransform);
                }

                if (_pokemon.FirstProperty.propertyEnum != PropertyEnum.无属性)
                {
                    _firstProperty.SetActive(true);
                    _firstProperty.GetComponent<Text>().text = _pokemon.info.firstPropertyEnum.ToString();
                }
                else
                {
                    _firstProperty.SetActive(false);
                    Debug.LogWarning("Pokemon没有第一属性!!!");
                }

                if (_pokemon.SecondProperty.propertyEnum != PropertyEnum.无属性)
                {
                    _secondProperty.SetActive(true);
                    _secondProperty.GetComponent<Text>().text = _pokemon.info.secondPropertyEnum.ToString();
                }
                else
                {
                    if (_secondProperty != null)
                    {
                        _secondProperty.SetActive(false);
                    }
                }

                //训练家
                if (_pokemon.info.trainer != null)
                {
                    _trainerText.text = _pokemon.info.trainer.name;
                }
                else
                {
                    Debug.LogWarning("查看的这只宝可梦没有训练家");
                    _trainerText.text = "";
                }

                //ID
                _idText.text = _pokemon.Uid;
                //exp Now
                _expNowText.text = _pokemon.info.expNow.ToString();
                //exp Need
                _expNowNeedText.text = _pokemon.info.expNeed.ToString();
                //特效
                if (_pokemon.Peculiarity != null)
                {
                    //特性名
                    _peculiarityNameText.text = _pokemon.Peculiarity.name;
                    //特效效果描述
                    _peculiarityDescText.text = _pokemon.Peculiarity.desc;
                }
                else
                {
                    _peculiarityNameText.text = "None";
                    _peculiarityDescText.text = "None";
                }

                //hp文字
                _hpText.text = $"{_pokemon.info.currentHealth}/{_pokemon.abilityValue.Health}";
                //hp-mask
                _hpMaskImage.fillAmount = (float)_pokemon.info.currentHealth / _pokemon.abilityValue.Health;
                //Obj Attack
                _objectAttackText.text = _pokemon.abilityValue.ObjectAttack.ToString();
                //Obj Defense
                _objectDefenseText.text = _pokemon.abilityValue.ObjectDefense.ToString();
                //Spe Attack
                _specialAttackText.text = _pokemon.abilityValue.SpecialAttack.ToString();
                //Spe Defense
                _specialDefenseText.text = _pokemon.abilityValue.SpecialDefense.ToString();
                //Speed
                _speedText.text = _pokemon.abilityValue.Speed.ToString();
            }
            else
            {
                Debug.LogWarning("Pokemon 未在当前Detail设置引用");
            }
        }

        public void set_pokemon(Pokemon pokemon)
        {
            _pokemon = pokemon;
        }
    }
}