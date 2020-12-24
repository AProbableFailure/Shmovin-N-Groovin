using ProjectVivid7.ECS;
using ProjectVivid7.Components.Renderable;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectVivid7.Components;
using ProjectVivid7.Components.Camera;
using ProjectVivid7.Components.Physics;
using Microsoft.Xna.Framework;
using ProjectVivid7.Utilities.Managers;
using ProjectVivid7.Utilities.Extensions;

namespace ProjectVivid7.Scenes
{
    public class TestingScene : Scene
    {
        public override void BuildScene()
        {
            var background = AddEntity("background");
            background.AddComponent(new Texture2DComponent("Test_Floor"));

            var player = AddEntity("player");
            player.AddComponent(new Texture2DComponent("SmileyWalk"));
            player.AddComponent(new ControllerComponent());

            var playerAnimations = player.AddComponent(new AnimationComponent());
            playerAnimations.AddAnimation("crouching", new Animation("SmileyWalk", 4, 4, 0.1f), () => player.GetComponent<MovementComponent>().Speed == 1f);
            playerAnimations.AddAnimation("walking", new Animation("SmileyWalk", 4, 4, 0.05f), () => player.GetComponent<MovementComponent>().Speed == 2f);
            playerAnimations.AddAnimation("sprinting", new Animation("SmileyWalk", 4, 4, 0.01f), () => player.GetComponent<MovementComponent>().Speed == 4f);

            player.AddComponent(new FollowCameraComponent());

            var rope = AddEntity("rope");
            Func<Vector2> firstAnchor = () => player.CenterPosition;//InputManager.MouseWorldPosition;
            Func<Vector2> secondAnchor = () => firstAnchor().DistanceTo(InputManager.MouseWorldPosition) <= 15 * 35 ? InputManager.MouseWorldPosition
                                      : (InputManager.MousePositionFromCenter.SafeNormalize() * 15 * 35) + firstAnchor();
            //secondAnchor = () => Vector2.Zero;

            rope.AddComponent(new RopeSimulationComponent(firstAnchor, secondAnchor, false));       //(15f, 35, 4f, false, firstAnchor, secondAnchor));//InputManager.MouseWorldPosition, new Vector2(40, 40)));

        }
    }
}
