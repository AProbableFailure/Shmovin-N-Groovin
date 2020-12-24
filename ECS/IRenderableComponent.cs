using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ECS
{
    interface IRenderableComponent
    {
        void DrawComponent(SpriteBatch spriteBatch);
    }
}
