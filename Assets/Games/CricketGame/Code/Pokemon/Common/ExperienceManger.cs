using System;
using System.Collections.Generic;

namespace Games.CricketGame.Code.Pokemon.Enum
{
    /// <summary>
    /// 用于计算宝可梦升级所需经验
    /// </summary>
    public static class ExperienceManger
    {
        private static Dictionary<ExperienceEnum, int> _expMap;

        public static int AlreadyExperience(PokemonData pokemon)
        {
            var level = pokemon.level;
            return (int)(Math.Pow(level, 3) + Math.Pow(level, 2) - (double)level / 4 +
                         _base_exp(pokemon.meta.experienceEnum));
        }
        public static int NeededExperience(PokemonData pokemon)
        {
            // 升级所需经验 = 
            var level = pokemon.level;
            if (level == 100)
            {//满级则不需要额外经验
                return -1;
            }
            level += 1;//否则计算下一级需要的经验总和 - 已经获得的经验
            return (int)(Math.Pow(level, 3) + Math.Pow(level, 2) - (double)level / 4 +
                         _base_exp(pokemon.meta.experienceEnum)) - pokemon.alreadyExperience;
        }


        /// <summary>
        /// 计算精灵战斗胜利获得的经验
        /// </summary>
        /// <param name="p1">胜利者</param>
        /// <param name="p2">失败者</param>
        /// <returns></returns>
        public static int AttackExperience(PokemonData p1, PokemonData p2)
        {
            //ToDo 后续再做数值上的优化
            double sub = Math.Sqrt((double)p2.level / p1.level); //等级差距
            var exp = _base_exp(p2.meta.experienceEnum); //被击败精灵提供的基础经验值

            return (int)sub * exp + exp;
        }

        private static int _base_exp(ExperienceEnum experienceEnum)
        {
            //ToDo 后续改成读文件 并且进行数值上的优化
            _expMap ??= new Dictionary<ExperienceEnum, int>
            {
                [ExperienceEnum.最快] = 10,
                [ExperienceEnum.较快] = 15,
                [ExperienceEnum.正常] = 20,
                [ExperienceEnum.较慢] = 35,
                [ExperienceEnum.最慢] = 50
            };

            return _expMap[experienceEnum];
        }
    }
}