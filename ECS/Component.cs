using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ECS
{
    public abstract class Component
    {
        public Entity ParentEntity { get; set; }
        public bool Enabled { get; set; } = true;
        public virtual void OnAddComponent(Entity parentEntity) => ParentEntity = parentEntity;

        // Component Lifecycle
            // OnAddComponent
            // Initialize
            // LoadContent
            // Update
            // Draw
    }
}
