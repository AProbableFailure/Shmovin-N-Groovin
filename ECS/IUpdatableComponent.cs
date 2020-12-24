using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ECS
{
    interface IUpdatableComponent
    {
        void UpdateComponent(GameTime gameTime);
    }
}
