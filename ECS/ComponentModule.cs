using ProjectVivid7.Components;
using ProjectVivid7.Components.Renderable;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ECS
{
    public abstract class ComponentModule
    {
        public List<Component> ModuleComponents { get; set; } = new List<Component>();
    }
}
