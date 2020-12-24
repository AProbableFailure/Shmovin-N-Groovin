using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.Components.Renderable
{
    public class Animation
    {
        public int CurrentFrame { get; set; }
        public int FrameCount { get; private set; }
        public int Columns { get; private set; }
        public int Rows { get; private set; }
        public float AnimationSpeed { get; set; }

        public string SpriteSheetPath { get; private set; }
        public Texture2D SpriteSheet { get; private set; }
        public int FrameWidth { get { return SpriteSheet.Width / Columns; } }
        public int FrameHeight { get { return SpriteSheet.Height / Rows; } }
        public Vector2 FrameSize { get { return new Vector2(FrameWidth, FrameHeight); } }

        public bool IsLooping { get; set; } = true;

        public Animation(string pSpriteSheet, int pColumns, int pRows, int pFrameCount, float pAnimationSpeed = 0.2f)
        {
            AnimationSpeed = pAnimationSpeed;

            FrameCount = pFrameCount;

            Columns = pColumns;
            Rows = pRows;
            //IsLooping = true;

            SpriteSheetPath = pSpriteSheet;
        }

        public Animation(string pSpriteSheet, int pColumns, int pRows, float pAnimationSpeed = 0.2f)
        {
            AnimationSpeed = pAnimationSpeed;

            FrameCount = pColumns * pRows;

            Columns = pColumns;
            Rows = pRows;
            //IsLooping = true;

            SpriteSheetPath = pSpriteSheet;
        }

        public void LoadAnimationContent(ContentManager content)
        {
            SpriteSheet = content.Load<Texture2D>(SpriteSheetPath);
        }
    }
}
