using System.Collections.Generic;
using PokemonGame.Code.UI;
using UnityEngine;

namespace PokemonGame.Code.Structs
{
    public class Pokemon : MonoBehaviour
    {
        private PokemonBase _pokemonBase;
        public Property FirstProperty => _pokemonBase.firstProperty;

        public Property SecondProperty => _pokemonBase.secondProperty;
        public Peculiarity Peculiarity => _pokemonBase.peculiarity;
        public Item Item => _pokemonBase.item;
        public string Uid => _pokemonBase.uid;
        public Trainer Trainer => _pokemonBase.trainer;
        public State State => _pokemonBase.state;
        public Character Character => _pokemonBase.character;

        public Skill Skill1 => _pokemonBase.skill1;
        public Skill Skill2 => _pokemonBase.skill2;
        public Skill Skill3 => _pokemonBase.skill3;
        public Skill Skill4 => _pokemonBase.skill4;


        [Header("初始化属性")] public PokemonInfo info;
        public AbilityValue abilityValue; //用于在inspector实时显示宝可梦的能力值 
        public AbilityValue raceValue; //用于在inspector实时显示宝可梦的种族值 
        public AbilityValue effortValue; //用于在inspector实时显示宝可梦的努力值 
        public AbilityValue individualValue; //用于在inspector实时显示宝可梦的个体值 
        [Header("图像")] public Sprite icon;
        public Sprite frontImage; //希望以后可以改成3D模型
        private SpriteRenderer _spriteRenderer;
        public GameObject pokemonModel;
        [Header("DEBUG")] [SerializeField] private PokemonCell pokemonCell;


        private void Awake()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }

            _spriteRenderer.sprite = frontImage;
            if (pokemonModel == null)
            {
                Debug.LogWarning("当前宝可梦没有指定预制体模型");
            }

            InitializePokemon();
        }

        private void InitializePokemon()
        {
            //用inspector上的信息初始化PokemonBase
            var race = new Dictionary<Ability, uint>
            {
                { Ability.Health, raceValue.Health },
                { Ability.ObjectAttack, raceValue.ObjectAttack },
                { Ability.ObjectDefense, raceValue.ObjectDefense },
                { Ability.SpecialAttack, raceValue.SpecialAttack },
                { Ability.SpecialDefense, raceValue.SpecialDefense },
                { Ability.Speed, raceValue.Speed }
            };
            var effort = new Dictionary<Ability, uint>
            {
                { Ability.Health, effortValue.Health },
                { Ability.ObjectAttack, effortValue.ObjectAttack },
                { Ability.ObjectDefense, effortValue.ObjectDefense },
                { Ability.SpecialAttack, effortValue.SpecialAttack },
                { Ability.SpecialDefense, effortValue.SpecialDefense },
                { Ability.Speed, effortValue.Speed }
            };
            var individual = new Dictionary<Ability, uint>
            {
                { Ability.Health, individualValue.Health },
                { Ability.ObjectAttack, individualValue.ObjectAttack },
                { Ability.ObjectDefense, individualValue.ObjectDefense },
                { Ability.SpecialAttack, individualValue.SpecialAttack },
                { Ability.SpecialDefense, individualValue.SpecialDefense },
                { Ability.Speed, individualValue.Speed }
            };
            _pokemonBase = PokemonBase.create_pokemon(
                name: info.name,
                otherName: info.otherName,
                id: info.id,
                characterEnum: info.character,
                firstPropertyEnum: info.firstPropertyEnum,
                secondPropertyEnum: info.secondPropertyEnum,
                abilityValue: null,
                raceValue: race,
                individualValue: individual,
                effortValue: effort,
                level: info.level,
                sex: info.sex,
                item: info.item,
                currentHealth: info.currentHealth,
                expNow: info.expNow,
                expNeed: info.expNeed,
                skillEnum1: info.skill1,
                skillEnum2: info.skill2,
                skillEnum3: info.skill3,
                skillEnum4: info.skill4,
                peculiarityEnum: info.peculiarityEnum,
                trainer: info.trainer,
                stateEnum: info.stateEnum
            );
            Synchronization(); //同步面板显示和实际存储信息
        }

        private void Synchronization()
        {
            _pokemonBase.update_ability(); //重新计算能力值

            abilityValue.Health = _pokemonBase.ability[Ability.Health];
            abilityValue.ObjectAttack = _pokemonBase.ability[Ability.ObjectAttack];
            abilityValue.ObjectDefense = _pokemonBase.ability[Ability.ObjectDefense];
            abilityValue.SpecialAttack = _pokemonBase.ability[Ability.SpecialAttack];
            abilityValue.SpecialDefense = _pokemonBase.ability[Ability.SpecialDefense];
            abilityValue.Speed = _pokemonBase.ability[Ability.Speed];

            raceValue.Health = _pokemonBase.race[Ability.Health];
            raceValue.ObjectAttack = _pokemonBase.race[Ability.ObjectAttack];
            raceValue.ObjectDefense = _pokemonBase.race[Ability.ObjectDefense];
            raceValue.SpecialAttack = _pokemonBase.race[Ability.SpecialAttack];
            raceValue.SpecialDefense = _pokemonBase.race[Ability.SpecialDefense];
            raceValue.Speed = _pokemonBase.race[Ability.Speed];

            effortValue.Health = _pokemonBase.effort[Ability.Health];
            effortValue.ObjectAttack = _pokemonBase.effort[Ability.ObjectAttack];
            effortValue.ObjectDefense = _pokemonBase.effort[Ability.ObjectDefense];
            effortValue.SpecialAttack = _pokemonBase.effort[Ability.SpecialAttack];
            effortValue.SpecialDefense = _pokemonBase.effort[Ability.SpecialDefense];
            effortValue.Speed = _pokemonBase.effort[Ability.Speed];

            individualValue.Health = _pokemonBase.individual[Ability.Health];
            individualValue.ObjectAttack = _pokemonBase.individual[Ability.ObjectAttack];
            individualValue.ObjectDefense = _pokemonBase.individual[Ability.ObjectDefense];
            individualValue.SpecialAttack = _pokemonBase.individual[Ability.SpecialAttack];
            individualValue.SpecialDefense = _pokemonBase.individual[Ability.SpecialDefense];
            individualValue.Speed = _pokemonBase.individual[Ability.Speed];

            //Info 更新
            info.id = _pokemonBase.id;
            info.name = _pokemonBase.name;
            info.otherName = _pokemonBase.otherName;
            info.level = _pokemonBase.level;
            info.firstPropertyEnum = FirstProperty.propertyEnum;
            info.secondPropertyEnum = SecondProperty.propertyEnum;
            info.stateEnum = State.stateEnum;
            info.sex = _pokemonBase.sex;
            info.currentHealth = _pokemonBase.current_health;
            info.item = _pokemonBase.item.item_enum;
            info.expNeed = _pokemonBase.exp_need;
            info.expNow = _pokemonBase.exp_now;
            //技能部分
            info.skill1 = _pokemonBase.skill1?.skillEnum ?? SkillEnum.None;
            info.skill2 = _pokemonBase.skill2?.skillEnum ?? SkillEnum.None;
            info.skill3 = _pokemonBase.skill3?.skillEnum ?? SkillEnum.None;
            info.skill4 = _pokemonBase.skill4?.skillEnum ?? SkillEnum.None;
            //特效部分
            info.peculiarityEnum = _pokemonBase.peculiarity?.peculiarityEnum ?? PeculiarityEnum.None;
        }

        private void Update()
        {
            Synchronization();
        }

        private void Start()
        {
            //ToDo 测试
            if (pokemonCell != null)
            {
                pokemonCell.set_pokemon(this);
            }
            
        }
    }
}