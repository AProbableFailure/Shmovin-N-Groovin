using Microsoft.Xna.Framework;
using ProjectVivid7.ECS;
using ProjectVivid7.Utilities.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.Components.Camera
{
    class FollowCameraComponent : Component, IUpdatableComponent
    {
        public void UpdateComponent(GameTime gameTime)
        {
            var sceneCameraPosition = ParentEntity.ParentScene.SceneCamera.SceneCameraPosition;

            ParentEntity.ParentScene.SceneCamera.SceneCameraPosition =
                Vector2.Lerp(sceneCameraPosition, ParentEntity.CenterPosition + InputManager.MousePositionFromCenterWithCameraZoom / 7, 0.25f);
        }
    }
}
