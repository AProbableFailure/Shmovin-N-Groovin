using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectVivid7.ECS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ECS.ECSUtilities
{
    public class ComponentList
    {
        Entity _entity;

        List<Component> _components = new List<Component>();
        List<IInitializableComponent> _initializableComponents = new List<IInitializableComponent>();
        List<IContentLoadableComponent> _loadableComponents = new List<IContentLoadableComponent>();
        List<IUpdatableComponent> _updatableComponents = new List<IUpdatableComponent>();
        List<IRenderableComponent> _renderableComponents = new List<IRenderableComponent>();

        internal List<Component> _componentsToAdd = new List<Component>();
        List<Component> _componentsToRemove = new List<Component>();
        List<Component> _tempBufferList = new List<Component>();

        public ComponentList(Entity entity)
        {
            _entity = entity;
        }

        public int Count => _components.Count;
        public Component this[int index] => _components[index];

        public void Add(Component component)
        {
            _componentsToAdd.Add(component);
        }
        public void Remove(Component component)
        {
            if (_componentsToRemove.Contains(component))
                return;

            if (_componentsToAdd.Contains(component))
            {
                _componentsToAdd.Remove(component);
                return;
            }

            _componentsToRemove.Add(component);
        }

        public void RemoveAllComponents()
        {
            //for (var i = 0; i < _components.Count; i++)
            //    HandleRemove(_components[i]);
            foreach (var component in _components) HandleRemove(component);

            _components.Clear();
            _initializableComponents.Clear();
            _loadableComponents.Clear();
            _updatableComponents.Clear();
            _renderableComponents.Clear();
            _componentsToAdd.Clear();
            _componentsToRemove.Clear();
        }

        void HandleRemove(Component component)
        {
            //_entity.ParentScene.RenderableComponents.Remove(component as RenderableComponent);
            if (component is IInitializableComponent initializable)
                _initializableComponents.Remove(initializable);
            if (component is IContentLoadableComponent loadable)
                _loadableComponents.Remove(loadable);
            if (component is IUpdatableComponent updatable)
                _updatableComponents.Remove(updatable);
            if (component is IRenderableComponent renderable)
                _renderableComponents.Remove(renderable);

            //component.OnRemovedFromEntity();
            component.ParentEntity = null;
        }

        public T GetComponent<T>(bool onlyInitializedComponents = false) where T : Component
        {
            foreach (var component in _components) 
                if (component is T TComponent) 
                    return TComponent;

            if (!onlyInitializedComponents)
                foreach (var component in _componentsToAdd)
                    if (component is T TComponent)
                        return TComponent;

            return null;
        }

        //public void GetComponents<T>(List<T> components) where T : class
        //{
        //    //for (var i = 0; i < _components.Length; i++)
        //    //{
        //    //    var component = _components.Buffer[i];
        //    //    if (component is T)
        //    //        components.Add(component as T);
        //    //}
        //    foreach (var component in components)
        //        if (component is T TComponent)
        //            return TComponent;

        //    // we also check the pending components just in case addComponent and getComponent are called in the same frame
        //    for (var i = 0; i < _componentsToAdd.Count; i++)
        //    {
        //        var component = _componentsToAdd[i];
        //        if (component is T)
        //            components.Add(component as T);
        //    }
        //}

        public void UpdateLists() //private
        {
            if (_componentsToRemove.Count > 0)
            {
                //Console.WriteLine(_componentsToRemove.Count);
                foreach (var component in _componentsToRemove)
                {
                    HandleRemove(component);
                    _components.Remove(component);
                }
                _componentsToRemove.Clear();
            }

            if (_componentsToAdd.Count > 0)
            {
                foreach (var component in _componentsToAdd)
                {
                    if (component is IInitializableComponent initializable)
                        _initializableComponents.Add(initializable);
                    if (component is IContentLoadableComponent loadable)
                        _loadableComponents.Add(loadable);
                    if (component is IRenderableComponent renderable)
                        _renderableComponents.Add(renderable);
                    if (component is IUpdatableComponent updatable)
                        _updatableComponents.Add(updatable);

                    _components.Add(component);
                    _tempBufferList.Add(component);
                }

                _componentsToAdd.Clear();

                for (var i = 0; i < _tempBufferList.Count; i++)
                {
                    //var component = _tempBufferList[i];
                    //component.OnAddComponent(_entity);
                    _tempBufferList[i].OnAddComponent(_entity);

                    // component.enabled checks both the Entity and the Component
                    //if (component.Enabled)
                    //component.OnEnabled();
                }

                _tempBufferList.Clear();
            }
        }

        public void InitializeComponents()
        {
            UpdateLists();

            foreach (var component in _initializableComponents)
                if ((component as Component).Enabled)
                    component.InitializeComponent();
        }

        public void LoadContentComponents(ContentManager content)
        {
            UpdateLists();

            foreach (var component in _loadableComponents)
                if ((component as Component).Enabled)
                    component.LoadContentComponent(content);
        }

        public void UpdateComponents(GameTime gameTime)
        {
            UpdateLists();

            foreach (var component in _updatableComponents)
                if ((component as Component).Enabled)
                    component.UpdateComponent(gameTime);
        }

        public void DrawComponents(SpriteBatch spriteBatch)
        {
            //UpdateLists();

            foreach (var component in _renderableComponents)
                if ((component as Component).Enabled)
                    component.DrawComponent(spriteBatch);
        }
    }
}
