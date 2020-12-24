using Microsoft.Xna.Framework;
using ProjectVivid7.Utilities.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ECS
{
    public class SceneCamera
    {
        public float Zoom { get; set; } = 1.25f;
        private const float minZoom = 0.75f;
        private const float maxZoom = 2.5f;

        public Vector2 SceneCameraPosition { get; set; } = Vector2.Zero;

        public Matrix PositionTransform { get { return Matrix.CreateTranslation(SceneCameraPosition.X, SceneCameraPosition.Y, 0); } }
        public Matrix ZoomScale { get { return Matrix.CreateScale(Zoom, Zoom, 1f); } }
        public Matrix ScreenTransform { get { return Matrix.CreateTranslation(WindowManager.ViewportSize.X / 2, WindowManager.ViewportSize.Y / 2, 0); } }
        public Matrix RawSceneCameraMatrix { get { return Matrix.Invert(PositionTransform) * ZoomScale * ScreenTransform; } }

        public virtual void UpdateSceneCamera(GameTime gameTime)
        {
            if (InputManager.MouseScrollingDirection == 1)
                Zoom += 2 * (maxZoom - Zoom) / 37.5f;

            // Zooming out
            else if (InputManager.MouseScrollingDirection == -1)
                Zoom += 2 * (minZoom - Zoom) / 37.5f;
        }
    }
}
