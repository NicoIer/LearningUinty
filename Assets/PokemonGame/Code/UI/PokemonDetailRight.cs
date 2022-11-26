using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.UI
{
    public class PokemonDetailRight : MonoBehaviour
    {
        [SerializeField] private PokemonDetail _pokemonDetail;
        private Pokemon _pokemon;
        [Header("Button")] [SerializeField] private Button backBtn;
        [SerializeField] private Button exitBtn;

        [Header("pokemon Model")] [SerializeField]
        private Camera pokemonCamera;

        [SerializeField] private GameObject pokemonModel;

        [Header("info")] [SerializeField] private Text nameText;
        [SerializeField] private Image sexImage;
        [SerializeField] private Text levelText;
        [SerializeField] private Image stateImage;
        [SerializeField] private Text stateText;
        [Header("Order")] [SerializeField] private Sprite noSel;
        [SerializeField] private Sprite sel;
        [SerializeField] private List<Image> ballImages;
        public uint curPokemon = 0;
        private uint _lastPokemon = 0;

        [Header("Item")] [SerializeField] private Text itemText;
        [SerializeField] private Image itemImage;

        [SerializeField] private List<SkillCell> skillCells;

        private void Awake()
        {
            if (_pokemonDetail == null)
            {
                _pokemonDetail = transform.parent.parent.GetComponent<PokemonDetail>();
            }
            
            if (backBtn == null)
            {
                backBtn = transform.Find("buttons").Find("back").GetComponent<Button>();
            }

            backBtn.onClick.AddListener(_pokemonDetail.OnBackButtonClicked);
            if (exitBtn == null)
            {
                exitBtn = transform.Find("buttons").Find("exit").GetComponent<Button>();
            }
            exitBtn.onClick.AddListener(_pokemonDetail.OnExitBtnClicked);

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
            if (nameText == null)
            {
                nameText = info.Find("name").GetComponent<Text>();
            }

            if (sexImage == null)
            {
                sexImage = info.Find("sex").GetComponent<Image>();
            }

            if (levelText == null)
            {
                levelText = info.Find("Level").Find("level").GetComponent<Text>();
            }

            if (stateText == null)
            {
                stateText = info.Find("state").Find("text").GetComponent<Text>();
            }

            if (stateImage == null)
            {
                stateImage = info.Find("state").GetComponent<Image>();
            }

            if (noSel == null || sel == null)
            {
                Debug.LogWarning("没有指定宝可梦详情页面的Order图片");
            }

            //6个球
            var order = detail.Find("Order");
            curPokemon = 0;
            if (ballImages == null)
            {
                ballImages = new List<Image>();
            }
            else
            {
                ballImages.Clear();
            }

            for (var i = 0; i < order.childCount; i++)
            {
                var image = order.GetChild(i).GetComponent<Image>();
                image.sprite = noSel;
                ballImages.Add(image);
            }

            ballImages[(int)curPokemon].sprite = sel;

            if (itemText == null)
            {
                itemText = detail.Find("Item").Find("text").GetComponent<Text>();
            }

            if (itemImage == null)
            {
                itemImage = detail.Find("Item").Find("icon").GetComponent<Image>();
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

        private void OnDisable()
        {
        }
        
        private void update_ui()
        {
            if (_pokemon != null)
            {
                //name
                nameText.text = _pokemon.info.otherName;
                //sex
                switch (_pokemon.info.sex)
                {
                    case Sex.None:
                        sexImage.color = new Color(1, 1, 1, 0);
                        break;
                    case Sex.Man:
                        sexImage.color = Color.blue;
                        break;
                    case Sex.Woman:
                        sexImage.color = Color.red;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                //level
                levelText.text = _pokemon.info.level.ToString();
                //state ToDo 完善这里的其他判断条件
                switch (_pokemon.info.state)
                {
                    case State.None:
                        stateImage.color = new Color(0, 0, 0, 0);
                        stateText.text = "";
                        break;
                    case State.Poisoning:
                        stateImage.color = Color.magenta;
                        stateText.text = State.Poisoning.ToString();
                        break;
                    case State.Sleeping:
                        stateImage.color = Color.white;
                        stateText.text = State.Sleeping.ToString();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                //精灵球的选择
                ballImages[(int)_lastPokemon].sprite = noSel;
                ballImages[(int)curPokemon].sprite = sel;
                //Item ToDo 完善这里
                if (_pokemon.info.item != ItemEnum.None)
                {
                    itemImage.color = new Color(0, 0, 0, 0);
                    itemText.text = "";
                }
                else
                {
                    itemImage.color = Color.black;
                    itemText.text =_pokemon.info.item.ToString();
                }
                //Skills ToDo 
            }
            else
            {
                Debug.LogWarning("PokemonDetail->Right持有的Pokemon为空!!");
            }
        }

        public void set_pokemon(Pokemon pokemon, uint index=0)
        {
            _pokemon = pokemon;
            _lastPokemon = curPokemon;
            curPokemon = index;
        }
    }
}