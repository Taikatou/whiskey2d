﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core
{
    [Serializable]
    public class Convex
    {

        public Vector Origin { get; set; }
        public float Rotation { get; set; }
       
        internal VectorSet Vectors { get; set; }
        internal VectorSet TranslatedVectors { get { return calculateTrans(); } }
       
        
        public Convex()
            : this(Vector.Zero)
        { }

        public Convex(Vector origin)
            : this(origin, 0)
        { }

        public Convex(Vector origin, float rotation)
            : this(origin, rotation, VectorSet.Square * 50)
        { }

        public Convex(Vector origin, float rotation, VectorSet set)
        {
            this.Origin = origin;
            this.Vectors = set;
            this.Rotation = rotation;
           
        }


        private VectorSet calculateTrans()
        {
            Matrix mat = Matrix.Identity;
            mat *= Matrix.CreateRotationZ(Rotation);
            mat *= Matrix.CreateTranslation(Origin.X, Origin.Y, 0);

            VectorSet tSet = new VectorSet();
            Vectors.forEachVector(v =>
            {
                Vector tV =  Vector2.Transform(v, mat);
                tSet.add(tV);
            });
            return tSet;
        }

        public Boolean isWithin(Vector vec)
        {

            //vec -= Origin; //remove origin from thought.

            List<Vector> vSet = calculateTrans().getAllVectors();

            int i;
            int j;
            bool result = false;

            

            for (i = 0, j = vSet.Count - 1; i < vSet.Count; j = i++)
            {
                if ((vSet[i].Y > vec.Y) != (vSet[j].Y > vec.Y) &&
                    (vec.X < (vSet[j].X - vSet[i].X) * (vec.Y - vSet[i].Y) / (vSet[j].Y - vSet[i].Y) + vSet[i].X))
                {
                    result = !result;
                }
            }
            return result;
        }

        public Boolean isWithin(Convex conv)
        {
            return getCollisionInfo(conv) != null;
        }

        public RayCollisionInfo getRayCollisionInfo(Vector rayStart, Vector rayDir)
        {

            VectorSet set = calculateTrans();
            Vector? pClosest = null;
            Edge eClosest = null;

            if (rayDir.Length > 0)
            {

                
                
                float distance = float.MaxValue;

                set.forEachEdge(edge =>
                {
                    float x = rayStart.X * edge.Unit.Y - rayStart.Y * edge.Unit.X - edge.V1.X * edge.Unit.Y + edge.V1.Y * edge.Unit.X;
                    x /= rayDir.Y * edge.Unit.X - rayDir.X * edge.Unit.Y;

                    float y = rayStart.X * rayDir.Y - rayStart.Y * rayDir.X + rayDir.X * edge.V1.Y - rayDir.Y * edge.V1.X;
                    y /= rayDir.Y * edge.Unit.X - rayDir.X * edge.Unit.Y;

                    if (x > 0 && y > 0 && y < edge.Length)
                    {

                        Vector p = edge.V1 + y * edge.Unit;
                        float testDistance = (p - rayStart).Length;

                        if (testDistance < distance)
                        {
                            distance = testDistance;
                            pClosest = p;
                            eClosest = edge;
                        }


                    }


                });
            }
            
            if (pClosest.HasValue)
            {
                return new RayCollisionInfo(rayStart, rayDir, eClosest.Normal, pClosest.Value);
            } else return null;
        }


        /// <summary>
        /// Run an SAT test against the other convex object
        /// </summary>
        /// <param name="conv"></param>
        /// <returns></returns>
        public CollisionInfo getCollisionInfo(Convex conv)
        {


            List<Vector> axisSet = new List<Vector>();
            VectorSet aSet = calculateTrans();
            VectorSet bSet = conv.calculateTrans();
            Vector mtv = Vector.One * 10000;
            bool colliding = true;

            aSet.forEachEdge(e => axisSet.Add(e.Normal));
            bSet.forEachEdge(e => axisSet.Add(e.Normal));

            foreach (Vector a in axisSet)
            {
                //create the axis
                Vector axis = a.Unit;

                float aMin = float.MaxValue, aMax = float.MinValue;
                float bMin = float.MaxValue, bMax = float.MinValue;

                //project conv, a, onto axis
                aSet.forEachVector(v =>
                {
                    float p = (v).dot(axis);
                    if (p < aMin) aMin = p;
                    if (p > aMax) aMax = p;
                });

                //project conv, b, onto axis
                bSet.forEachVector(v =>
                {
                    float p = ( v).dot(axis);
                    if (p < bMin) bMin = p;
                    if (p > bMax) bMax = p;
                });

                //are a and b overlapping?
                if ((aMin > bMin && aMin < bMax) ||
                    (aMax > bMin && aMax < bMax) ||
                    (bMin > aMin && bMin < aMax) ||
                    (bMax > aMin && bMax < aMax)
                    )
                {
                    //they are overlapping
                    //calculate overlap vector
                    float overlap = Math.Min(aMax, bMax) - Math.Max(aMin, bMin);
                    if (aMax < bMax)
                    {
                        overlap *= -1;
                    }
                    

                    if (Math.Abs(overlap) < mtv.Length)
                    {
                        mtv = axis * overlap;
                    }

                }
                else
                {
                    //they are not overlapping. no collision can be occuring.
                    colliding = false;
                    break;
                }

            }



            CollisionInfo collInfo = new CollisionInfo(mtv);
            if (!colliding)
            {
                return null;
            }
            else return collInfo;
        }



        public void render(SpriteBatch spriteBatch, Matrix transform, RenderHints hints)
        {

            render(spriteBatch.GraphicsDevice, transform, hints);
        }

        public class ConvexRenderStuff
        {
            public Convex Convex { get; set; }
            public GraphicsDevice GraphcisDevice { get; set; }
            public Matrix Transform { get; set; }
            public RenderHints Hints { get; set; }

        }
        public static List<ConvexRenderStuff> readyToBeDrawn = new List<ConvexRenderStuff>();


        public static void begin()
        {
            readyToBeDrawn.Clear();
        }
        public void renderWhenReady(GraphicsDevice graphicsDevice, Matrix transform, RenderHints hints)
        {
            ConvexRenderStuff stuff = new ConvexRenderStuff();
            stuff.Convex = this;
            stuff.GraphcisDevice = graphicsDevice;
            stuff.Transform = transform;
            stuff.Hints = hints;
            readyToBeDrawn.Add(stuff);
        }

        public void render(GraphicsDevice graphicsDevice, Matrix transform, RenderHints hints)
        {
            
            

            PrimitiveBatch primBatch = PrimitiveBatch.getInstance(graphicsDevice);
            primBatch.Transform = transform;
            primBatch.Begin(hints.PrimitiveType);
            VectorSet vSet = calculateTrans();
            vSet.forEachVector(v =>
            {
                primBatch.AddVertex(v, hints.Color);
            });
            primBatch.End();
        }

    }
}
