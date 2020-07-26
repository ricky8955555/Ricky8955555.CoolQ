using System;

namespace Ricky8955555.CoolQ
{
    internal static class RandomUtilities
    {
        private static readonly Random Random = new Random();

        internal static double NextDouble(double minValue, double maxValue) => (Random.NextDouble() * (maxValue - minValue)) + minValue;

        internal static long Next(long minValue, long maxValue) => (long)Math.Round(NextDouble(minValue, maxValue));

        internal static T RandomOption<T>(params T[] options) => options[Random.Next(0, options.Length)];
    }
}