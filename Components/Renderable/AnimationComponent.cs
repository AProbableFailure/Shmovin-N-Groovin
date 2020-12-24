using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVivid7.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectVivid7.Components.Renderable
{
    class AnimationComponent : Component, IInitializableComponent, IContentLoadableComponent, IUpdatableComponent, IRenderableComponent
    {
        public List<(Func<bool>, string)> AnimationTriggers { get; set; } = new List<(Func<bool>, string)>();
        public Dictionary<string, Animation> EntityAnimations { get; set; } = new Dictionary<string, Animation>();
        public Animation CurrentAnimation { get; set; }
        private float timer;

        public AnimationComponent()
        {

        }
        public AnimationComponent(Dictionary<string, Animation> entityAnimations)
        {
            EntityAnimations = entityAnimations;
        }

        public void AddAnimation(string animationName, Animation animation, Func<bool> trigger)
        {
            EntityAnimations.Add(animationName, animation);
            AnimationTriggers.Add((trigger, animationName));
        }

        public void SetAnimation() // TODO : make it tied to state machine
        {
            foreach (var animationTrigger in AnimationTriggers)
            {
                if (animationTrigger.Item1())
                {
                    Play(EntityAnimations[animationTrigger.Item2]);
                }
            }
        }

        public void Play(Animation pAnimation)
        {
            if (CurrentAnimation == pAnimation)
                return;

            ParentEntity.EntitySize = pAnimation.FrameSize;


            CurrentAnimation = pAnimation;
            CurrentAnimation.CurrentFrame = 0;

            timer = 0f;
        }
        public void Stop()
        {
            timer = 0f;
            CurrentAnimation.CurrentFrame = 0;
        }

        public void InitializeComponent()
        {
            if (ParentEntity.HasComponent<Texture2DComponent>()) ParentEntity.RemoveComponent<Texture2DComponent>();
        }

        public void LoadContentComponent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            foreach (var animation in EntityAnimations.Values)
            {
                animation.LoadAnimationContent(content);
            }

            CurrentAnimation = EntityAnimations.Values.First();
            ParentEntity.EntitySize = CurrentAnimation.FrameSize;
        }

        public void UpdateComponent(GameTime gameTime)
        {
            SetAnimation();

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > CurrentAnimation.AnimationSpeed)
            {
                timer = 0f;

                CurrentAnimation.CurrentFrame++;

                if (CurrentAnimation.CurrentFrame >= CurrentAnimation.FrameCount)
                    CurrentAnimation.CurrentFrame = 0;
            }
        }

        public void DrawComponent(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentAnimation.SpriteSheet
                            , ParentEntity.Position
                            , new Rectangle(CurrentAnimation.FrameWidth * (CurrentAnimation.CurrentFrame % CurrentAnimation.Columns)
                                            , CurrentAnimation.FrameHeight * (int)MathF.Floor(CurrentAnimation.CurrentFrame / CurrentAnimation.Columns)
                                            , CurrentAnimation.FrameWidth
                                            , CurrentAnimation.FrameHeight)
                            , Color.White
                            , 0f, Vector2.Zero
                            , 1
                            , ParentEntity.FacingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally
                            , 1); //Height);
        }
    }
}
