using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.UI
{
    public class PokemonDetailRight : MonoBehaviour
    {
        [SerializeField] private PokemonDetail pokemonDetail;
        private Pokemon _pokemon;
        private Button _backBtn;
        private Button _exitBtn;

        [Header("pokemon Model")] [SerializeField]
        private Camera pokemonCamera;

        [SerializeField] private GameObject pokemonModel;

        private Text _nameText;
        private Image _sexImage;
        private Text _levelText;
        private Image _stateImage;
        private Text _stateText;
        [Header("Order")] [SerializeField] private Sprite noSel;
        [SerializeField] private Sprite sel;
        private List<Image> _ballImages;
        public uint curPokemon = 0;
        private uint _lastPokemon = 0;
        [SerializeField] private Transform itemTransform;
        private Text _itemText;
        private Image _itemImage;

        [SerializeField] private List<SkillCell> skillCells;

        private void Awake()
        {
            if (pokemonDetail == null)
            {
                pokemonDetail = transform.parent.parent.GetComponent<PokemonDetail>();
            }

            if (_backBtn == null)
            {
                _backBtn = transform.Find("buttons").Find("back").GetComponent<Button>();
            }

            _backBtn.onClick.AddListener(pokemonDetail.OnBackButtonClicked);
            if (_exitBtn == null)
            {
                _exitBtn = transform.Find("buttons").Find("exit").GetComponent<Button>();
            }

            _exitBtn.onClick.AddListener(pokemonDetail.OnExitBtnClicked);

            var detail = transform.Find("Detail");
            if (pokemonCamera == null)
            {
                pokemonCamera = detail.Find("Pokemon").Find("camera").GetComponent<Camera>();
            }

            if (pokemonModel == null)
            {
                pokemonModel = detail.Find("Pokemon").Find("pokemon").gameObject;
            }

            var info = detail.Find("Info");
            if (_nameText == null)
            {
                _nameText = info.Find("name").GetComponent<Text>();
            }

            if (_sexImage == null)
            {
                _sexImage = info.Find("sex").GetComponent<Image>();
            }

            if (_levelText == null)
            {
                _levelText = info.Find("Level").Find("level").GetComponent<Text>();
            }

            if (_stateText == null)
            {
                _stateText = info.Find("state").Find("text").GetComponent<Text>();
            }

            if (_stateImage == null)
            {
                _stateImage = info.Find("state").GetComponent<Image>();
            }

            if (noSel == null || sel == null)
            {
                Debug.LogWarning("没有指定宝可梦详情页面的Order图片");
            }

            //6个球
            var order = detail.Find("Order");
            curPokemon = 0;
            if (_ballImages == null)
            {
                _ballImages = new List<Image>();
            }
            else
            {
                _ballImages.Clear();
            }

            for (var i = 0; i < order.childCount; i++)
            {
                var image = order.GetChild(i).GetComponent<Image>();
                image.sprite = noSel;
                _ballImages.Add(image);
            }

            _ballImages[(int)curPokemon].sprite = sel;
            itemTransform = detail.Find("Item");
            if (_itemText == null)
            {
                _itemText = itemTransform.Find("text").GetComponent<Text>();
            }

            if (_itemImage == null)
            {
                _itemImage = itemTransform.Find("icon").GetComponent<Image>();
            }

            var skills = detail.Find("Skills");
            if (skillCells == null)
            {
                skillCells = new List<SkillCell>();
            }
            else
            {
                skillCells.Clear();
            }

            for (var i = 0; i < skills.childCount; i++)
            {
                if (i == 4)
                {
                    Debug.LogWarning("技能数量超过四个!!");
                    break;
                }

                skillCells.Add(skills.GetChild(i).GetComponent<SkillCell>());
            }
        }

        private void OnEnable()
        {
            update_ui();
        }


        private void update_ui()
        {
            if (_pokemon != null)
            {
                //name
                _nameText.text = _pokemon.info.otherName;
                //sex
                switch (_pokemon.info.sex)
                {
                    case Sex.None:
                        _sexImage.color = new Color(1, 1, 1, 0);
                        break;
                    case Sex.Man:
                        _sexImage.color = Color.blue;
                        break;
                    case Sex.Woman:
                        _sexImage.color = Color.red;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                //level
                _levelText.text = _pokemon.info.level.ToString();
                //state ToDo 完善这里的其他判断条件
                switch (_pokemon.info.stateEnum)
                {
                    case StateEnum.None:
                        _stateImage.color = new Color(0, 0, 0, 0);
                        _stateText.text = "";
                        break;
                    case StateEnum.Poisoning:
                        _stateImage.color = Color.magenta;
                        _stateText.text = StateEnum.Poisoning.ToString();
                        break;
                    case StateEnum.Sleeping:
                        _stateImage.color = Color.white;
                        _stateText.text = StateEnum.Sleeping.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                //精灵球的选择
                _ballImages[(int)_lastPokemon].sprite = noSel;
                _ballImages[(int)curPokemon].sprite = sel;
                //Item ToDo 完善这里
                if (_pokemon.Item == null || _pokemon.Item.item_enum==ItemEnum.None)
                {
                    itemTransform.gameObject.SetActive(false);
                    _itemImage.color = new Color(0, 0, 0, 0);
                    _itemText.text = "";
                }
                else
                {
                    itemTransform.gameObject.SetActive(true);
                    _itemImage.color = Color.black;
                    _itemText.text = _pokemon.Item.name;
                }
                //Skills ToDo 
            }
            else
            {
                Debug.LogWarning("PokemonDetail->Right持有的Pokemon为空!!");
            }
        }

        public void set_pokemon(Pokemon pokemon, uint index = 0)
        {
            _pokemon = pokemon;
            _lastPokemon = curPokemon;
            curPokemon = index;
        }
    }
}