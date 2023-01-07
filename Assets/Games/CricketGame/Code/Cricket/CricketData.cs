using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Games.CricketGame.Cricket_;
using Games.CricketGame.Manager;
using Newtonsoft.Json;
using UnityEngine;
using Random = System.Random;

namespace Games.CricketGame.Code.Cricket_
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

            var values = Enum.GetValues(typeof(PersonalityEnum));
            PersonalityEnum personalityEnum = (PersonalityEnum)values.GetValue(_random.Next(values.Length));
            values = Enum.GetValues(typeof(CharacterEnum));
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
        public Character character;

        //性格
        public Personality personality;

        //异常状态 ToDo 将异常状态加入考虑
        public StateEnum stateEnum;

        #endregion

        #region 基础信息

        public string name;
        public int level;
        public int alreadyExperience;
        public bool sex;

        #endregion

        #region 技能信息

        public List<Skill> skills;

        #endregion

        #region Action

        public Action<float, float> expRateChangeAction;
        public Action<string, int> levelUpAction;
        public Action<float, float> healthRateChangeAction;

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

        [JsonIgnore] public int defaultHealth { get; private set; }
        [JsonIgnore] public int defaultAttack { get; private set; }
        [JsonIgnore] public int defaultDefense { get; private set; }
        [JsonIgnore] public int defaultSpecialAttack { get; private set; }
        [JsonIgnore] public int defaultSpecialDefense { get; private set; }
        [JsonIgnore] public int defaultSpeed { get; private set; }

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
            character = Character.Find(characterEnum);
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

            CalculateDefault();
        }

        #endregion

        #region 随机初始化 Method

        public void RandomInit()
        {
            level = _random_level();
            RandomExp();
            RandomIndividual();
            CalculateDefault();
            foreach (Skill skill in skills)
            {
                //ToDo 不应该由他进行初始化
                skill.InitMeta();
            }
        }

        public void RandomInit(int level)
        {
            this.level = level;
            RandomExp();
            RandomIndividual();
            CalculateDefault();
            foreach (Skill skill in skills)
            {
                //ToDo 不应该由他进行初始化
                skill.InitMeta();
            }
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

        public void CalculateDefault()
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
            attackAbility = (int)((meta.attackRace * 2 + attackIndividual + Math.Sqrt(attackEffort)) * levelRate + 10);
            defenseAbility =
                (int)((meta.defenseRace * 2 + defenseIndividual + Math.Sqrt(defenseEffort)) * levelRate + 10);
            specialAttackAbility =
                (int)((meta.specialAttackRace * 2 + specialAttackIndividual + Math.Sqrt(specialAttackEffort)) *
                      levelRate +
                      10);
            specialDefenseAbility =
                (int)((meta.specialDefenseRace * 2 + specialDefenseIndividual + Math.Sqrt(specialDefenseEffort)) *
                      levelRate +
                      10);
            speedAbility = (int)((meta.speedRace * 2 + speedIndividual + Math.Sqrt(speedEffort)) * levelRate + 10);

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
            defaultHealth = healthAbility;
            defaultAttack = attackAbility;
            defaultDefense = defenseAbility;
            defaultSpecialAttack = specialAttackAbility;
            defaultSpecialDefense = specialDefenseAbility;
            defaultSpeed = speedAbility;
        }

        private void _levelUp()
        {
            level += 1;
            //Todo 做进化 还有其他设置
            Debug.Log("进化了");
            // meta = PokemonDataMeta.Find(meta.nextLevel[0]);
            levelUpAction?.Invoke(name, level);
            CalculateDefault();
        }

        public async void ChangeExp(int exp)
        {
            var totalTimes = 30;
            var needed = NeededExp();
            var total = LevelTotalExp();
            float one_exp;
            float cur_exp;
            while (exp >= needed)
            {
                exp -= needed;
                one_exp = (float)needed / totalTimes;
                cur_exp = LevelAttainedExp();
                for (var i = 1; i != totalTimes + 1; i++)
                {
                    cur_exp += one_exp;
                    expRateChangeAction.Invoke(cur_exp, total);
                    await UniTask.Delay(20);
                }

                alreadyExperience += needed;
                _levelUp();
                if (level == 100)
                {
                    expRateChangeAction.Invoke(1, 1);
                    return;
                }
            }

            cur_exp = LevelAttainedExp();
            one_exp = (float)exp / totalTimes;
            for (var i = 1; i != totalTimes + 1; i++)
            {
                cur_exp += one_exp;
                expRateChangeAction.Invoke(cur_exp, total);
                await UniTask.Delay(20);
            }

            alreadyExperience += exp;
        }


        public async UniTask ChangeHealth(int damage)
        {
            //ToDo 这里很丑陋 后面来改一下结构
            if (healthAbility <= 0)
            {
                return;
            }

            int totalTimes = 30;
            float tempHealth = defaultHealth;
            //将伤害分成很多次进行事件调用,以慢慢的更新UI
            float one_damage = (float)damage / totalTimes;
            for (var i = 1; i != totalTimes + 1; i++)
            {
                tempHealth = healthAbility - i * one_damage;
                if (tempHealth <= 0)
                {
                    healthAbility = 0;
                    healthRateChangeAction?.Invoke(healthAbility, defaultHealth);
                    return;
                }

                if (tempHealth >= defaultHealth)
                {
                    healthAbility = defaultHealth;
                    healthRateChangeAction?.Invoke(healthAbility, defaultHealth);
                    return;
                }

                healthRateChangeAction?.Invoke(tempHealth, defaultHealth);
                await UniTask.WaitForFixedUpdate();
            }

            healthAbility -= damage;
            if (healthAbility <= 0)
            {
                healthAbility = 0;
            }
            else if (healthAbility >= defaultHealth)
            {
                healthAbility = defaultHealth;
            }

            healthRateChangeAction?.Invoke(healthAbility, defaultHealth);
        }
    }
}