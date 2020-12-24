using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.Utilities.Extensions
{
    public static class Vector2Extensions
    {
        // Conversion Methods
        public static Point ToPoint(this Vector2 v)
            => new Point((int)v.X, (int)v.Y);
        public static Vector2 ToVector2(this Point p)
            => new Vector2(p.X, p.Y);
        public static Vector2 RectangleSizeToVector2(this Rectangle r) 
            => new Vector2(r.Width, r.Height);
        public static Vector2 RectanglePositionToVector2(this Rectangle r)
            => new Vector2(r.X, r.Y);
        public static Vector2 CreateFromAngleAndMagnitude(float angle, float magnitude)
            => new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * magnitude;


        //-------------------
        // Location Methods
        //-------------------
        public static bool IsBetween(this Vector2 v, float x1, float x2, float y1, float y2)
            => v.X.IsBetween(x1, x2) && v.Y.IsBetween(y1, y2);
        public static bool IsBetween(this Vector2 v, Vector2 v1, Vector2 v2)
            => v.IsBetween(v1.X, v2.X, v1.Y, v2.Y);
        public static bool IsIn(this Vector2 v, Rectangle rect)
            => v.X.IsBetween(rect.X, rect.X + rect.Width) && v.Y.IsBetween(rect.Y, rect.Y + rect.Height);
        
        public static bool IsAround(this Vector2 v, float r, float x1, float x2, float y1, float y2)
            => v.X.IsBetween(Math.Min(x1, x2) - r, Math.Max(x1, x2) + r)
            && v.Y.IsBetween(Math.Min(y1, y2) - r, Math.Max(y1, y2) + r);
        public static bool IsAround(this Vector2 v, float r, Vector2 v1, Vector2 v2)
            => v.IsAround(r, v1.X, v2.X, v1.Y, v2.Y);
        public static bool IsAround(this Vector2 v, float r, float x, float y)
            => v.IsAround(r, x, x, y, y);
        public static bool IsAround(this Vector2 v, float r, Vector2 v1)
            => v.IsAround(r, v1.X, v1.Y);
        
        public static float DistanceTo(this Vector2 v1, Vector2 v2)
            => MathF.Sqrt(MathF.Pow(v1.X - v2.X, 2) + MathF.Pow(v1.Y - v2.Y, 2));

        public static float AngleTo(this Vector2 v1, Vector2 v2)
            => MathF.Atan2(v2.Y - v1.Y, v2.X - v1.X);

        public static Vector2 Midpoint(this Vector2 v1, Vector2 v2)
            => (v1 + v2) / 2;

        public static Vector2 SafeNormalize(this Vector2 v)
            => (v == Vector2.Zero) ? Vector2.Zero : Vector2.Normalize(v);

        public static Vector2 RotateAround(this Vector2 v, Vector2 origin, float radians)
            => Vector2.Transform(v - origin, Matrix.CreateRotationZ(radians)) + origin;
    }
}
