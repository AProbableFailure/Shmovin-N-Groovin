using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ProjectVivid7.ECS.ECSUtilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.ECS
{
    public class Scene
    {
        public EntityList Entities { get; set; }
        public SceneCamera SceneCamera { get; set; } = new SceneCamera();
        //private ContentManager Content;

        public Scene()
        {
            Entities = new EntityList(this);
        }

        public void ChangeScene(Scene changeScene)
        {
            Game1.CurrentScene = changeScene;
            changeScene.BuildScene();
            changeScene.InitializeScene();
            //changeScene.LoadContentScene(Content);
        }

        public virtual void BuildScene() // the canvas on which you build on
        {

        }

        public void InitializeScene()
        {
            Entities.InitializeEntities();
        }
        public void LoadContentScene(ContentManager content)
        {
            Entities.LoadContentEntities(content);
        }
        public void UpdateScene(GameTime gameTime)
        {
            SceneCamera.UpdateSceneCamera(gameTime);
            Entities.UpdateEntities(gameTime);
        }
        public void DrawScene(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(transformMatrix : SceneCamera.RawSceneCameraMatrix);
            Entities.DrawEntities(spriteBatch);
            spriteBatch.End();
        }



        //public bool HasEntity<T>() where T : Entity => Entities.GetComponent<T>() != null;

        //public T GetComponent<T>() where T : Component => Components.GetComponent<T>();
        //public bool TryGetComponent<T>(out T component) where T : Component
        //{
        //    component = GetComponent<T>();
        //    return component != null;
        //}

        public Entity AddEntity(string name, Vector2 position = default)
        {
            var entity = new Entity(name, position);
            Entities.Add(entity);
            entity.OnAddEntity(this);
            return entity;
        }
        public void RemoveEntity(Entity entity) => Entities.Remove(entity);
    }
}
