using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectVivid7.ECS.ECSUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ECS
{
    public class Entity
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public bool Enabled { get; set; } = true;
        public bool FacingRight { get; set; } = true; 
        public Scene ParentScene;
        //private ContentManager Content;

        //public Vector2 Position { get; set; }
        public Transform Transform { get; set; }
        public Vector2 Position { get => Transform.Position; set => Transform.Position = value; }
        public Vector2 EntitySize { get; set; } = Vector2.Zero;
        public Vector2 CenterPosition { get => new Vector2(Transform.Position.X + EntitySize.X / 2, Transform.Position.Y + EntitySize.Y / 2); }

        //public List<Component> Components { get; set; } = new List<Component>();
        public ComponentList Components { get; set; }// = new ComponentList(entity: this);

        public Entity(string name, Vector2 position)
        {
            Name = name;
            Transform = new Transform(position, Vector2.One);//Position = position;
            Components = new ComponentList(this);
        }

        public void InitializeEntity()
        {
            Components.InitializeComponents();
        }
        public void LoadContentEntity(ContentManager content)
        {
            Components.LoadContentComponents(content);
        }
        public void UpdateEntity(GameTime gameTime)
        {
            //Console.WriteLine(Components.Count);
            Components.UpdateComponents(gameTime);
        }
        public void DrawEntity(SpriteBatch spriteBatch)
        {
            Components.DrawComponents(spriteBatch);
        }



        public void OnAddEntity(Scene parentScene)
        {
            ParentScene = parentScene;
        }

        //--------------------
        // Component Methods
        //--------------------
        public bool HasComponent<T>() where T : Component => Components.GetComponent<T>() != null;

        public T GetComponent<T>() where T : Component => Components.GetComponent<T>();
        public bool TryGetComponent<T>(out T component) where T : Component
        {
            component = GetComponent<T>();
            return component != null;
        }

        public T AddComponent<T>() where T : Component, new() => AddComponent(new T());
        public T AddComponent<T>(T component) where T : Component
        {
            //component.ParentEntity = this;
            Components.Add(component);
            //Components.UpdateLists();
            //component.OnAddComponent(this);
            return component;
        }

        public T GetOrAddComponent<T>() where T : Component, new()
        {
            var component = Components.GetComponent<T>(true);
            if (component == null)
                component = AddComponent<T>();
            return component;
        }
        
        public void RemoveComponent<T>(T component) where T : Component => Components.Remove(component);
        public void RemoveComponent<T>() where T : Component
        {
            RemoveComponent(GetComponent<T>());
        }
    }
}
