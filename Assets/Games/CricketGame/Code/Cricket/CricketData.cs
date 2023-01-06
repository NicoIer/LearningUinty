using System;
using Games.CricketGame.Manager.Code.Manager;
using Newtonsoft.Json;
using Random = System.Random;

namespace Games.CricketGame.Manager.Code.Pokemon
{
    [Serializable]
    public class CricketData
    {
        #region STATIC

        private static readonly Random _random = new();

        public static CricketData GetRandom(CricketEnum @enum)
        {
            var level = _random.Next(1, 101);
            var hi = _random.Next(0, 32);
            var ai = _random.Next(0, 32);
            var di = _random.Next(0, 32);
            var sai = _random.Next(0, 32);
            var sdi = _random.Next(0, 32);
            var si = _random.Next(0, 32);
            var he = _random.Next(0, 32);
            var ae = _random.Next(0, 32);
            var de = _random.Next(0, 32);
            var sae = _random.Next(0, 32);
            var sde = _random.Next(0, 32);
            var se = _random.Next(0, 32);

            var values = System.Enum.GetValues(typeof(PersonalityEnum));
            PersonalityEnum personalityEnum = (PersonalityEnum)values.GetValue(_random.Next(values.Length));
            values = System.Enum.GetValues(typeof(CharacterEnum));
            CharacterEnum characterEnum = (CharacterEnum)values.GetValue(_random.Next(values.Length));

            return new CricketData(
                @enum,
                @enum.ToString(),
                personalityEnum,
                characterEnum,
                level,
                hi,
                ai,
                di,
                sai,
                sdi,
                si,
                he,
                ae,
                de,
                sae,
                sde,
                se);
        }

        #endregion

        #region 特殊信息

        //基本数据(图鉴和种族值)

        public CricketDataMeta meta;

        //特性
        public Character.Character character;

        //性格
        public Personality personality;

        #endregion

        #region 基础信息

        public string name;
        public int level;
        public int alreadyExperience;
        public bool sex = true;

        #endregion

        #region Action

        public Action<int> damageAction;
        public Action<int> expAction;

        #endregion

        #region 能力值

        #region 当前值

        public int healthAbility;
        public int attackAbility;
        public int defenseAbility;
        public int specialAttackAbility;
        public int specialDefenseAbility;
        public int speedAbility;

        #endregion

        #region 默认值

        [JsonIgnore] public int defalut_health { get; private set; }
        [JsonIgnore] public int defalut_attack { get; private set; }
        [JsonIgnore] public int _defenseAbility { get; private set; }
        [JsonIgnore] public int _specialAttackAbility { get; private set; }
        [JsonIgnore] public int _specialDefenseAbility { get; private set; }
        [JsonIgnore] public int _speedAbility { get; private set; }

        #endregion

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

        #region Construct Method

        public CricketData(CricketEnum cricketEnum,
            string name,
            PersonalityEnum personalityEnum,
            CharacterEnum characterEnum,
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
            int speedEffort,
            bool sex = true
        )
        {
            this.sex = sex;
            this.level = level;
            this.name = name;
            personality = Personality.Find(personalityEnum);
            character = Character.Character.Find(characterEnum);
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
            meta = CricketDataMeta.Find(cricketEnum);

            UpdateDefault();
        }

        #endregion

        #region 随机初始化 Method

        public void RandomInit()
        {
            level = _random_level();
            RandomExp();
            RandomIndividual();
            UpdateDefault();
        }

        public void RandomExp()
        {
            var levelExp = ExperienceManger.LevelExp(meta.experienceEnum, level);
            alreadyExperience =
                levelExp + _random.Next(0, ExperienceManger.LevelUpTotalExp(meta.experienceEnum, level));
        }

        public void RandomIndividual()
        {
            healthIndividual = _random.Next(0, 32);
            attackIndividual = _random.Next(0, 32);
            defenseIndividual = _random.Next(0, 32);
            specialAttackIndividual = _random.Next(0, 32);
            specialDefenseIndividual = _random.Next(0, 32);
            speedIndividual = _random.Next(0, 32);
        }

        public void RandomEffort()
        {
            throw new NotImplementedException();
        }

        private int _random_level()
        {
            return _random.Next(1, 101);
        }

        private int _random_individual()
        {
            return _random.Next(0, 32);
        }

        #endregion

        #region Attribut Method

        #region Exp

        public int NeededExp() =>
            ExperienceManger.LevelUpNeededExperience(meta.experienceEnum, level, alreadyExperience);

        public int LevelTotalExp() =>
            ExperienceManger.LevelUpTotalExp(meta.experienceEnum, level);

        /// <summary>
        /// 当前等级已经获得的经验
        /// </summary>
        /// <returns></returns>
        public int LevelAttainedExp() => LevelTotalExp() - NeededExp();

        #endregion

        #endregion

        public void UpdateDefault()
        {
            if (string.IsNullOrEmpty(name))
            {
                name = meta.cricketEnum.ToString();
            }

            if (level == 0)
            {
                level = _random_level();
            }


            var levelRate = level / 100.0;
            healthAbility = (int)((meta.healthRace * 2 + healthIndividual + Math.Sqrt(healthEffort)) * levelRate + 15 +
                                  level);
            attackAbility = (int)((meta.healthRace * 2 + attackIndividual + Math.Sqrt(attackEffort)) * levelRate + 10);
            defenseAbility = (int)((meta.healthRace * 2 + defenseIndividual + Math.Sqrt(speedEffort)) * levelRate + 10);
            specialAttackAbility =
                (int)((meta.healthRace * 2 + specialAttackIndividual + Math.Sqrt(specialAttackEffort)) * levelRate +
                      10);
            specialDefenseAbility =
                (int)((meta.healthRace * 2 + specialDefenseIndividual + Math.Sqrt(specialDefenseEffort)) * levelRate +
                      10);
            speedAbility = (int)((meta.healthRace * 2 + speedIndividual + Math.Sqrt(speedEffort)) * levelRate + 10);

            foreach (var effect in personality.effects)
            {
                var effectRate = (1 + effect.percent / 100);
                switch (effect.abilityEnum)
                {
                    case AbilityEnum.生命:
                        healthAbility = (int)(healthAbility * effectRate);
                        break;
                    case AbilityEnum.攻击:
                        attackAbility = (int)(attackAbility * effectRate);
                        break;
                    case AbilityEnum.防御:
                        defenseAbility = (int)(defenseAbility * effectRate);
                        break;
                    case AbilityEnum.特殊攻击:
                        specialAttackAbility = (int)(specialAttackAbility * effectRate);
                        break;
                    case AbilityEnum.特殊防御:
                        specialDefenseAbility = (int)(specialDefenseAbility * effectRate);
                        break;
                    case AbilityEnum.速度:
                        speedAbility = (int)(speedAbility * effectRate);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            //计算默认值
            defalut_health = healthAbility;
            defalut_attack = attackAbility;
            _defenseAbility = defenseAbility;
            _specialAttackAbility = specialAttackAbility;
            _specialDefenseAbility = specialDefenseAbility;
            _speedAbility = speedAbility;
        }

        public void LevelUp()
        {
            level += 1;
            //Todo 做进化 还有其他设置
            // meta = PokemonDataMeta.Find(meta.nextLevel[0]);
            UpdateDefault();
        }

        public void AttainExp(int exp)
        {
            expAction.Invoke(exp);
        }

        public void DoDamage(int damage)
        {
            damageAction.Invoke(damage); //先处理事件 再扣血
        }
    }
}