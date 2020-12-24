using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.Utilities.Extensions
{
    public static class SpriteBatchExtensions
    {
        public static void DrawCurve(this SpriteBatch spriteBatch, Vector2[] vertexPositions, Color color, float thickness = 1, float layerDepth = 0)
        {
            for (int i = 0; i < vertexPositions.Length - 1; i++)
            {
                spriteBatch.DrawLine(vertexPositions[i], vertexPositions[i + 1], color, thickness, layerDepth);
                spriteBatch.DrawCircle(vertexPositions[i], 3f, 8, Color.Black, 3f);
            }
        }
    }
}
