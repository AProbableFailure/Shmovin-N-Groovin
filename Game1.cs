using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectVivid7.ECS;
using ProjectVivid7.Scenes;
using ProjectVivid7.Utilities.Managers;
using System;

namespace ProjectVivid7
{
    public class Game1 : Game
    {
        private ContentManager _content;
        public static GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static Scene CurrentScene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _content = Content;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            WindowManager.ViewportSize = new Vector2(2650, 1440);

            CurrentScene = new TestingScene();
            CurrentScene.BuildScene();

            CurrentScene.InitializeScene();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            CurrentScene.LoadContentScene(_content);
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Update();

            if (InputManager.IsInput(InputManager.Down, Inputs.Exit))
                Exit();

            if (InputManager.IsInput(InputManager.Triggered, Inputs.Fire1))
                Console.WriteLine("Triggered");
            if (InputManager.IsInput(InputManager.Down, Inputs.Fire1))
                Console.WriteLine("Down");
            if (InputManager.IsInput(InputManager.Released, Inputs.Fire1))
                Console.WriteLine("Released");

            if (InputManager.IsKey(InputManager.Triggered, Keys.D7))
                WindowManager.ViewportSize = new Vector2(1024, 1024);
            if (InputManager.IsKey(InputManager.Triggered, Keys.D8))
                WindowManager.ViewportSize = new Vector2(1920, 1280);
            if (InputManager.IsKey(InputManager.Triggered, Keys.D9))
                WindowManager.ViewportSize = new Vector2(2560, 1440);
            if (InputManager.IsKey(InputManager.Triggered, Keys.D0))
                WindowManager.ViewportSize = new Vector2(3840, 2160);

            if (InputManager.IsKey(InputManager.Triggered, Keys.O))
                CurrentScene.ChangeScene(new TestingScene());
            if (InputManager.IsKey(InputManager.Triggered, Keys.P))
                CurrentScene.ChangeScene(new TestingScene2());

            CurrentScene.UpdateScene(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            CurrentScene.DrawScene(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}
