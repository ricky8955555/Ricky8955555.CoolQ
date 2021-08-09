using System;
using System.Collections.Generic;
using System.Linq;

namespace Ricky8955555.CoolQ
{
    public static class RandomUtilities
    {
        private static readonly Random Random = new();

        public static double NextDouble(double minValue, double maxValue) => (Random.NextDouble() * (maxValue - minValue)) + minValue;

        public static long Next(long minValue, long maxValue) => (long)Math.Round(NextDouble(minValue, maxValue));

        public static T RandomOption<T>(params T[] options) => options[Random.Next(0, options.Length)];

        public static T RandomOption<T>(IEnumerable<T> options) => options.ElementAt(Random.Next(0, options.Count()));
    }
}