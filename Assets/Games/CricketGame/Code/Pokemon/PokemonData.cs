using System;
using Games.CricketGame.Code.Pokemon.Enum;
using UnityEngine;

namespace Games.CricketGame.Code.Pokemon
{
    [Serializable]
    public class PokemonData
    {
        //基本数据(图鉴和种族值)
        private PokemonDataMeta _data;
        //访问属性
        public PokemonDataMeta meta => _data;

        //性格
        public PersonalityEnum personalityEnum;
        public string name;
        public int level;
        public int alreadyExperience;

        public int healthAbility;
        public int attackAbility;
        public int defenseAbility;
        public int specialAttackAbility;
        public int specialDefenseAbility;
        public int speedAbility;

        public int healthIndividual;
        public int attackIndividual;
        public int defenseIndividual;
        public int specialAttackIndividual;
        public int specialDefenseIndividual;
        public int speedIndividual;

        public int healthEffort;
        public int attackEffort;
        public int specialAttackEffort;
        public int specialDefenseEffort;
        public int defenseEffort;
        public int speedEffort;

        public PokemonData(PokemonEnum pokemonEnum,
            int level,
            int healthIndividual,
            int attackIndividual,
            int defenseIndividual,
            int specialAttackIndividual,
            int specialDefenseIndividual,
            int speedIndividual,
            int healthEffort,
            int attackEffort,
            int defenseEffort,
            int specialAttackEffort,
            int specialDefenseEffort,
            int speedEffort
        )
        {
            this.level = level;

            this.healthIndividual = healthIndividual;
            this.attackIndividual = attackIndividual;
            this.defenseIndividual = defenseIndividual;
            this.specialAttackIndividual = specialAttackIndividual;
            this.specialDefenseIndividual = specialDefenseIndividual;
            this.speedIndividual = speedIndividual;

            this.healthEffort = healthEffort;
            this.attackEffort = attackEffort;
            this.defenseEffort = defenseEffort;
            this.specialAttackEffort = specialAttackEffort;
            this.specialDefenseEffort = specialDefenseEffort;
            this.speedEffort = speedEffort;

            _data = PokemonDataMeta.Find(pokemonEnum);
            //ToDo 计算默认能力值
            _cal_default();
        }

        private void _cal_default()
        {
            throw new NotImplementedException();
        }

        public void LevelUp()
        {
            throw new NotImplementedException();
        }
    }
}