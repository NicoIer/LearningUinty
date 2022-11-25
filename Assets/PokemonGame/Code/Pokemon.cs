using System;
using System.Collections.Generic;
using System.Text;
using PokemonGame.UI;
using UnityEngine;

namespace PokemonGame
{
    [Serializable]
    public struct PokemonInfo
    {
        public string name;
        public string otherName;
        public uint id;
        public uint level;
        public State state;
        public CharacterEnum character;
        public Property firstProperty;
        public Property secondProperty;
        public Sex sex;
        public uint currentHealth;
        public ItemEnum item;
    }

    public class Pokemon : MonoBehaviour
    {
        private PokemonBase _pokemonBase;
        public PokemonInfo info;
        [Header("图像")]
        public Sprite icon;
        public Sprite frontImage;//希望以后可以改成3D模型
        private SpriteRenderer _spriteRenderer;
        [Header("初始化属性")]
        public AbilityValue abilityValue; //用于在inspector实时显示宝可梦的能力值 
        public AbilityValue raceValue; //用于在inspector实时显示宝可梦的种族值 
        public AbilityValue effortValue; //用于在inspector实时显示宝可梦的努力值 
        public AbilityValue individualValue; //用于在inspector实时显示宝可梦的个体值 
        [Header("测试")]
        [SerializeField] public PokemonCell pokemonCell;


        private void Awake()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }

            _spriteRenderer.sprite = frontImage;
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
                firstProperty: info.firstProperty,
                secondProperty: info.secondProperty,
                abilityValue: null,
                raceValue: race,
                individualValue: individual,
                effortValue: effort,
                level: info.level,
                sex: info.sex,
                item:info.item,
                currentHealth:info.currentHealth
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
            info.name = _pokemonBase.name;
            info.otherName = _pokemonBase.otherName;
            info.level = _pokemonBase.level;
            info.firstProperty = _pokemonBase.firstProperty;
            info.secondProperty = _pokemonBase.secondProperty;
            info.state = _pokemonBase.state;
            info.sex = _pokemonBase.sex;
            info.currentHealth = _pokemonBase.current_health;
            info.item = _pokemonBase.item.item_enum;
        }

        private void Update()
        {
            Synchronization();
            //ToDo 测试
            pokemonCell.set_pokemon(this);
        }
    }
}