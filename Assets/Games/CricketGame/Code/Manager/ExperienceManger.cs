using System;
using System.Collections.Generic;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Cricket_;

namespace Games.CricketGame.Manager
{
    /// <summary>
    /// 用于计算宝可梦升级所需经验
    /// </summary>
    public static class ExperienceManger
    {
        private static Dictionary<ExperienceEnum, int> _expMap;

        public static int LevelUpTotalExp(ExperienceEnum experienceEnum, int level)
        {
            if (level == 100)
                return 0;
            return LevelExp(experienceEnum, level + 1) - LevelExp(experienceEnum, level);
        }

        /// <summary>
        /// 升到级下一级还需要的经验
        /// </summary>
        /// <returns></returns>
        public static int LevelUpNeededExperience(ExperienceEnum experienceEnum, int level, int attainedExp)
        {
            // 升级所需经验 = 
            if (level == 100)
            {
                //满级则不需要额外经验
                return 0;
            }
            //否则计算下一级需要的经验总和 - 已经获得的经验
            return LevelExp(experienceEnum, level+1) - attainedExp;
        }

        /// <summary>
        /// 对应等级所需经验
        /// </summary>
        /// <param name="experienceEnum"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int LevelExp(ExperienceEnum experienceEnum, int level)
        {
            return (int)(Math.Pow(level, 3) + Math.Pow(level, 2) - (double)level / 4 +
                         _base_exp(experienceEnum));
        }

        /// <summary>
        /// 计算精灵战斗胜利获得的经验
        /// </summary>
        /// <param name="p1">胜利者</param>
        /// <param name="p2">失败者</param>
        /// <returns></returns>
        public static int AttackExperience(CricketData p1, CricketData p2)
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