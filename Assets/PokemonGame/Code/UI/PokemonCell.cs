using System;
using UnityEngine;
using UnityEngine.UI;

namespace PokemonGame.UI
{
    public class PokemonCell : MonoBehaviour
    {
        private Pokemon _pokemon;
        public byte index;
        [SerializeField] private PokemonSel pokemonSel;
        [SerializeField] private GameObject detail;
        [SerializeField] private Image icon;
        [SerializeField] private Text pokemonName;
        [SerializeField] private Text levelText;
        [SerializeField] private Image hpMask;
        [SerializeField] private Text hpText;
        [SerializeField] private Image sex;
        [SerializeField] private Image item;
        [SerializeField] private Button button;

        private void Awake()
        {
            //ToDo 完善Null的处理

            if (detail == null)
            {
                detail = transform.Find("detail").gameObject;
            }

            if (_pokemon == null)
            {
                detail.SetActive(false);
            }

            if (icon == null)
            {
                icon = detail.transform.Find("icon").GetComponent<Image>();
            }

            if (pokemonName == null)
            {
                pokemonName = detail.transform.Find("name").GetComponent<Text>();
            }

            if (levelText == null)
            {
                levelText = detail.transform.Find("level").transform.Find("num").GetComponent<Text>();
            }

            if (hpMask == null)
            {
                hpMask = detail.transform.Find("hp").transform.Find("mask").GetComponent<Image>();
            }

            if (hpText == null)
            {
                hpText = detail.transform.Find("hp-text").GetComponent<Text>();
            }

            if (sex == null)
            {
                sex = detail.transform.Find("sex").GetComponent<Image>();
            }

            if (item == null)
            {
                item = detail.transform.Find("item").GetComponent<Image>();
            }

            if (pokemonSel == null)
            {
                pokemonSel = transform.parent.parent.GetComponent<PokemonSel>();
            }

            if (button == null)
            {
                button = GetComponent<Button>();
                button.onClick.AddListener(() => pokemonSel.OnPokemonCellClicked(_pokemon,index));
            }

            update_ui();
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
                detail.SetActive(true);
                //icon
                if (_pokemon.icon != null)
                {
                    icon.sprite = _pokemon.icon;
                }
                //name
                pokemonName.text = _pokemon.info.otherName;
                //level
                levelText.text = _pokemon.info.level.ToString();
                //hp_mask
                hpMask.fillAmount =
                    _pokemon.info.currentHealth / (_pokemon.abilityValue.Health + Mathf.Epsilon);
                //hp_text
                hpText.text = $"{_pokemon.info.currentHealth}/{_pokemon.abilityValue.Health}";
                //sex
                switch (_pokemon.info.sex)
                {
                    case Sex.None:
                        sex.color = new Color(1, 1, 1, 0);
                        break;
                    case Sex.Man:
                        sex.color = Color.blue;
                        break;
                    case Sex.Woman:
                        sex.color = Color.red;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                //item
                item.color = _pokemon.info.item == ItemEnum.None ? new Color(1, 1, 1, 0) : new Color(1, 1, 1, 1);
            }
            else
            {
                detail.SetActive(false);
            }
        }

        public void set_pokemon(Pokemon pokemon)
        {
            _pokemon = pokemon;
        }
    }
}