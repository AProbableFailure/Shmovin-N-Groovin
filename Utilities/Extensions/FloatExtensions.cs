using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.Utilities.Extensions
{
    public static class FloatExtensions
    {
        public static float Clamp(this float f, float min, float max) 
            => MathHelper.Clamp(f, Math.Min(min, max), Math.Max(min, max));

        public static bool IsBetween(this float f, float lower, float upper)
            => Math.Min(lower, upper) < f
            && f < Math.Max(lower, upper);
    }
}
