using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ECS
{
    interface IContentLoadableComponent
    {
        void LoadContentComponent(ContentManager content);
    }
}
