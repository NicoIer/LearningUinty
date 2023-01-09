using System.Collections.Generic;
using Games.CricketGame.Cricket_;

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
    }
}