using Microsoft.Xna.Framework;
using ProjectVivid7.ComponentModules;
using ProjectVivid7.Components;
using ProjectVivid7.Components.Camera;
using ProjectVivid7.Components.Physics;
using ProjectVivid7.Components.Renderable;
using ProjectVivid7.ECS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.Entities
{
    class PlayerEntity : Entity
    {
        public PlayerEntity(string name, Vector2 position = default) : base(name, position)
        {
            AddComponentModule(new PlayerComponentModule(this));
        }
    }
}
