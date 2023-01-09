using System.Collections.Generic;
using Games.CricketGame.Cricket_;
using Games.CricketGame.Npc_;
using Games.CricketGame.Player_;
using Nico.Common;

namespace Games.CricketGame.Manager
{
    public static class CompareManager
    {
        private static readonly Dictionary<PriorityEnum, int> _speedMap = new()
        {
            { PriorityEnum.超先手, 2 },
            { PriorityEnum.先手, 1 },
            { PriorityEnum.正常手, 0 },
            { PriorityEnum.后手, -1 }
        };

        public static bool ComparePriority(PriorityEnum p1, PriorityEnum p2)
        {
            return _speedMap[p1] > _speedMap[p2];
        }
        /// <summary>
        /// 比较技能出手先后
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static bool CompareSkillSpeed(CricketData c1,CricketData c2,Skill s1,Skill s2)
        {
            if (ComparePriority(s1.meta.priority, s2.meta.priority))
            {
                //技能优先级高?
                return true;
            }

            if (c1.speedAbility > c2.speedAbility)
            {
                //速度快?
                return true;
            }

            if (c1.speedAbility == c2.speedAbility)
            {
                return RandomManager.Probability(0, 1) > 0.5;
            }

            return false;
        }

        public static bool ComparePlayerRun(Npc npc,CricketData c1,CricketData c2)
        {//ToDo fix it
            return true;
        }

        public static bool CompareNpcRun(CricketData c1, CricketData c2)
        {
            return true;
        }
    }
}