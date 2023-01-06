using System;
using System.Collections.Generic;

namespace Nico.Common
{
    public static class RandomManager
    {
        private static Random _random = new();
        private static List<Random> randoms;
        private static bool _initialized = false;

        public static int Next(int min, int max)
        {
            return _random.Next(min, max);
        }

        public static float Probability(int min, int max)
        {
            if (min < 0)
            {
                min = 0;
            }

            if (max > 100)
            {
                max = 100;
            }
            return _random.Next(min, max) / 100f;
        }

        private static void Initialize()
        {
            _initialized = true;
            randoms = new List<Random>();
            randoms.Add(new Random());
        }
    }
}