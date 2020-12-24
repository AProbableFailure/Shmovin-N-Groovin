using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectVivid7.ECS;
using ProjectVivid7.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectVivid7.Components.Physics
{
    class RopeSimulationComponent : Component, IUpdatableComponent, IRenderableComponent
    {
        private List<RopeSegment> ropeSegments = new List<RopeSegment>();
        private float ropeSegmentLength;// = 15f;//0.25f;
        private int ropeVertexAmount;// = 35;
        private float ropeWidth;// = 4f;
        private bool elastic;// = true;
        private int updateAmount;
        public Func<Vector2> FirstAnchor { get; set; }
        public Func<Vector2> SecondAnchor { get; set; }

        public RopeSimulationComponent(Func<Vector2> firstAnchor, Func<Vector2> secondAnchor, bool elastic = true, int updateAmount = 10 /*50*/, float ropeSegmentLength = 15f, int ropeVertexAmount = 35, float ropeWidth = 4f)//(float ropeSegmentLength, int ropeVertexAmount, float ropeWidth, bool elastic, Func<Vector2> firstAnchor, Func<Vector2> secondAnchor)
        {
            this.ropeSegmentLength = ropeSegmentLength;
            this.ropeVertexAmount = ropeVertexAmount;
            this.ropeWidth = ropeWidth;
            this.elastic = elastic;
            this.updateAmount = updateAmount;

            FirstAnchor = firstAnchor;
            SecondAnchor = secondAnchor;
            var ropeStartPoint = firstAnchor();

            for (int i = 0; i < ropeVertexAmount; i++)
            {
                ropeSegments.Add(new RopeSegment(ropeStartPoint));
                ropeStartPoint.Y += ropeSegmentLength;
            }
        }

        public void UpdateComponent(GameTime gameTime)
        {
            Vector2 forceGravity = new Vector2(0f, 9.8f * 5);

            for (int i = 1; i < ropeVertexAmount; i++)
            {
                RopeSegment firstSegment = ropeSegments[i];
                Vector2 velocity = firstSegment.posNow - firstSegment.posOld;
                firstSegment.posOld = firstSegment.posNow;
                firstSegment.posNow += velocity;
                firstSegment.posNow += forceGravity * (float)gameTime.ElapsedGameTime.TotalSeconds;//Time.fixedDeltaTime;
                ropeSegments[i] = firstSegment;
            }

            for (int i = 0; i < updateAmount; i++)
            {
                ApplyConstraint();
            }
        }

        private void ApplyConstraint()
        {
            //Constrant to Mouse
            var firstSegment = ropeSegments[0];
            firstSegment.posNow = FirstAnchor();//AttatchedEntity.AttatchedScene.GetEntity("test-entity").CenterPosition;//FirstAnchor;//InputManager.MouseWorldPosition;
            ropeSegments[0] = firstSegment;

            var lastSegment = ropeSegments[^1];
            float ropeLength = ropeSegmentLength * ropeVertexAmount;

            lastSegment.posNow = SecondAnchor();
            ropeSegments[^1] = lastSegment;

            for (int i = 0; i < ropeVertexAmount - 1; i++)
            {
                var firstSeg = ropeSegments[i];
                var secondSeg = ropeSegments[i + 1];

                float dist = (firstSeg.posNow - secondSeg.posNow).Length();
                float error = dist - ropeSegmentLength;
                Vector2 changeDir = Vector2.Normalize(firstSeg.posNow - secondSeg.posNow);

                Vector2 changeAmount = changeDir * error;
                firstSeg.posNow -= changeAmount * 0.5f * Math.Sign(i);
                ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.5f; // originally had no 0.5f at i = 0
                ropeSegments[i + 1] = secondSeg;
            }
        }

        public void DrawComponent(SpriteBatch spriteBatch)
        {
            Vector2[] ropePositions = new Vector2[ropeVertexAmount];
            for (int i = 0; i < ropeVertexAmount; i++)
            {
                ropePositions[i] = ropeSegments[i].posNow;
            }

            spriteBatch.DrawCurve(ropePositions, Color.Red, ropeWidth);
        }
    }

    struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            posNow = pos;
            posOld = pos;
        }
    }
}
