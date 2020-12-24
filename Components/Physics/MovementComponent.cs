using Microsoft.Xna.Framework;
using ProjectVivid7.ECS;
using ProjectVivid7.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.Components.Physics
{
    class MovementComponent : Component, IUpdatableComponent
    {
        public Vector2 VelocityDirection { get; set; } = Vector2.Zero;
        public float Speed { get; set; } = 0;
        public Vector2 Velocity
        {
            get => VelocityDirection * Speed;
            set
            {
                VelocityDirection = value.SafeNormalize();//Vector2.Normalize(value);
                Speed = VelocityDirection == Vector2.Zero ? 0 : value.Length();
            }
        }

        public void UpdateComponent(GameTime gameTime)
        {
            //Console.WriteLine("ha");
            ParentEntity.Position += Velocity;
        }
    }
}
