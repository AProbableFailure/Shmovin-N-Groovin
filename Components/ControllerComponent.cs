using Microsoft.Xna.Framework;
using ProjectVivid7.Components.Physics;
using ProjectVivid7.ECS;
using ProjectVivid7.Utilities.Extensions;
using ProjectVivid7.Utilities.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.Components
{
    class ControllerComponent : Component, IInitializableComponent, IUpdatableComponent
    {
        MovementComponent Movement { get; set; }
        const float crouchSpeed = 1f;
        const float walkSpeed = 2f;
        const float sprintSpeed = 4f;

        public void InitializeComponent()
        {
            Movement = ParentEntity.GetOrAddComponent<MovementComponent>();
        }

        public void UpdateComponent(GameTime gameTime)
        {
            Move();
        }

        public void Move()
        {
            float horIn = Convert.ToInt16(InputManager.IsInput(InputManager.Down, Inputs.Right))
                        - Convert.ToInt16(InputManager.IsInput(InputManager.Down, Inputs.Left));
            float verIn = Convert.ToInt16(InputManager.IsInput(InputManager.Down, Inputs.Down))
                        - Convert.ToInt16(InputManager.IsInput(InputManager.Down, Inputs.Up));

            Movement.VelocityDirection = new Vector2(horIn, verIn).SafeNormalize(); //!(horIn == 0 && verIn == 0) ? Vector2.Normalize(new Vector2(horIn, verIn)) : Vector2.Zero;
            Movement.Speed = Movement.VelocityDirection == Vector2.Zero ? 0
                            : InputManager.IsInput(InputManager.Down, Inputs.Sprint) ? sprintSpeed
                            : InputManager.IsInput(InputManager.Down, Inputs.Crouch) ? crouchSpeed
                            : walkSpeed;

            if (Movement.VelocityDirection.X != 0f)
            {
                ParentEntity.FacingRight = Movement.VelocityDirection.X > 0;
            }
        }
    }
}
