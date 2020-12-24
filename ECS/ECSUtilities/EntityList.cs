using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ECS.ECSUtilities
{
    public class EntityList
    {
        Scene _scene;

        List<Entity> _entities = new List<Entity>();

        internal List<Entity> _entitiesToAdd = new List<Entity>();
        List<Entity> _entitiesToRemove = new List<Entity>();

        public EntityList(Scene scene)
        {
            _scene = scene;
        }

        public int Count => _entities.Count;
        public Entity this[int index] => _entities[index];

        public void Add(Entity entity)
        {
            _entitiesToAdd.Add(entity);
        }
        public void Remove(Entity entity)
        {
            if (_entitiesToRemove.Contains(entity))
                return;

            if (_entitiesToAdd.Contains(entity))
            {
                _entitiesToAdd.Remove(entity);
                return;
            }

            _entitiesToRemove.Add(entity);
        }

        public void RemoveAllComponents()
        {
            //for (var i = 0; i < _components.Count; i++)
            //    HandleRemove(_components[i]);
            foreach (var entity in _entities) HandleRemove(entity);

            _entities.Clear();
            _entitiesToAdd.Clear();
            _entitiesToRemove.Clear();
        }

        void HandleRemove(Entity entity)
        {
            //component.OnRemovedFromEntity();
            entity.ParentScene = null;
        }

        //public T GetEntity<T>(bool onlyInitializedEntities = false) where T : Component 
        //{
        //    foreach (var entity in _entities)
        //        if (entity is T TEntity)
        //            return TEntity;

        //    if (!onlyInitializedEntities)
        //        foreach (var entity in _entities)
        //            if (entity is T TEntity)
        //                return TEntity;

        //    return null;
        //}

        void UpdateLists()
        {
            if (_entitiesToRemove.Count > 0)
            {
                foreach (var component in _entitiesToRemove)
                {
                    HandleRemove(component);
                    _entitiesToRemove.Remove(component);
                }
                _entitiesToRemove.Clear();
            }

            if (_entitiesToAdd.Count > 0)
            {
                foreach (var entity in _entitiesToAdd)
                {
                    _entities.Add(entity);
                    //_tempBufferList.Add(component);
                }

                _entitiesToAdd.Clear();
            }
        }

        public void InitializeEntities()
        {
            UpdateLists();

            foreach (var entity in _entities)
                if (entity.Enabled)
                    entity.InitializeEntity();
        }

        public void LoadContentEntities(ContentManager content)
        {
            UpdateLists();

            foreach (var entity in _entities)
                if (entity.Enabled)
                    entity.LoadContentEntity(content);
        }

        public void UpdateEntities(GameTime gameTime)
        {
            UpdateLists();

            foreach (var entity in _entities)
                if (entity.Enabled)
                    entity.UpdateEntity(gameTime);
        }

        public void DrawEntities(SpriteBatch spriteBatch)
        {
            foreach (var entity in _entities)
                if (entity.Enabled)
                    entity.DrawEntity(spriteBatch);
        }
    }
}
