using ProjectVivid7.Components;
using ProjectVivid7.Components.Camera;
using ProjectVivid7.Components.Physics;
using ProjectVivid7.Components.Renderable;
using ProjectVivid7.ECS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ComponentModules
{
    class PlayerComponentModule : ComponentModule
    {
        public PlayerComponentModule(Entity parentEntity)
        {
            ModuleComponents.Add(new Texture2DComponent("SmileyWalk"));
            ModuleComponents.Add(new ControllerComponent());

            var playerAnimations = new AnimationComponent();
            playerAnimations.AddAnimation("crouching", new Animation("SmileyWalk", 4, 4, 0.1f), () => parentEntity.GetComponent<MovementComponent>().Speed == 1f);
            playerAnimations.AddAnimation("walking", new Animation("SmileyWalk", 4, 4, 0.05f), () => parentEntity.GetComponent<MovementComponent>().Speed == 2f || parentEntity.GetComponent<MovementComponent>().Speed == 0f);
            playerAnimations.AddAnimation("sprinting", new Animation("SmileyWalk", 4, 4, 0.01f), () => parentEntity.GetComponent<MovementComponent>().Speed == 4f);

            ModuleComponents.Add(playerAnimations);

            //ModuleComponents.Add(new FollowCameraComponent());
        }
    }
}
