using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using ProjectVivid7.ECS;
using ProjectVivid7.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.Components.Renderable
{
    class Texture2DComponent : Component, IRenderableComponent, IContentLoadableComponent
    {
        public Texture2D Texture { get; set; }
        private string FilePath;

        public Texture2DComponent(string filePath)
        {
            FilePath = filePath;
        }

        public override void OnAddComponent(Entity parentEntity)
        {
            base.OnAddComponent(parentEntity);
        }

        public void LoadContentComponent(ContentManager content)
        {
            Texture = content.Load<Texture2D>(FilePath);
            ParentEntity.EntitySize = Texture.Bounds.RectangleSizeToVector2();
        }

        public void DrawComponent(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawRectangle(Vector2.Zero, Vector2.One * 50, Color.Red, 5f);
            
            spriteBatch.Draw(Texture, ParentEntity.Position, Color.White);
        }
    }
}
