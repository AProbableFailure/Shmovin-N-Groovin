using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ECS
{
    public class Transform
    {
        public Vector2 Position { get; set; } 
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }

        public Transform(Vector2 position, Vector2 scale, float rotation = 0)
        {
            Position = position;
            Scale = scale;
            Rotation = rotation;
        }
    }
}
