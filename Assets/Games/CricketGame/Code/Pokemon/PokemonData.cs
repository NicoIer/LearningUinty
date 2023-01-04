using System;
using Games.CricketGame.Code.Pokemon.Enum;
using UnityEngine;
using Random = System.Random;

namespace Games.CricketGame.Code.Pokemon
{
    [Serializable]
    public class PokemonData
    {
        public static Random random = new Random();

        public static PokemonData random_init(PokemonEnum @enum)
        {
            var level = random.Next(1, 101);
            var hi = random.Next(0, 32);
            var ai = random.Next(0, 32);
            var di = random.Next(0, 32);
            var sai = random.Next(0, 32);
            var sdi = random.Next(0, 32);
            var si = random.Next(0, 32);
            var he = random.Next(0, 32);
            var ae = random.Next(0, 32);
            var de = random.Next(0, 32);
            var sae = random.Next(0, 32);
            var sde = random.Next(0, 32);
            var se = random.Next(0, 32);

            var values = System.Enum.GetValues(typeof(PersonalityEnum));
            PersonalityEnum personalityEnum = (PersonalityEnum)values.GetValue(random.Next(values.Length));
            return new PokemonData(
                @enum,
                @enum.ToString(),
                personalityEnum,
                level,
                hi, ai, di,
                sai, sdi, si, he, ae,
                de, sae, sde, se);
        }

        //基本数据(图鉴和种族值)
        private PokemonDataMeta _meta_data;

        //访问属性
        public PokemonDataMeta meta => _meta_data;

        //性格
        public PersonalityEnum personalityEnum;
        public Personality personality { get; private set; }
        public string name;
        public int level;
        public int alreadyExperience;

        #region 能力值

        public int healthAbility;
        public int attackAbility;
        public int defenseAbility;
        public int specialAttackAbility;
        public int specialDefenseAbility;
        public int speedAbility;

        #endregion

        #region 个体值

        public int healthIndividual;
        public int attackIndividual;
        public int defenseIndividual;
        public int specialAttackIndividual;
        public int specialDefenseIndividual;
        public int speedIndividual;

        #endregion

        #region 努力值

        public int healthEffort;
        public int attackEffort;
        public int specialAttackEffort;
        public int specialDefenseEffort;
        public int defenseEffort;
        public int speedEffort;

        #endregion


        public PokemonData(PokemonEnum pokemonEnum,
            string name,
            PersonalityEnum personalityEnum,
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
            this.name = name;
            this.personalityEnum = personalityEnum;
            personality = Personality.Find(personalityEnum);
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
            _meta_data = PokemonDataMeta.Find(pokemonEnum);

            _cal_default();
        }

        private void _cal_default()
        {
            //ToDo 计算默认能力值
            alreadyExperience = ExperienceManger.NeededExperience(this);
            healthAbility = level * healthIndividual + healthEffort / 4;
        }

        public void LevelUp()
        {
        }
    }
}