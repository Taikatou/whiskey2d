﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core
{

    /// <summary>
    /// Simple solution for rectangular collision.
    /// </summary>
    public class Bounds
    {

        private Vector position;
        private Vector size;

        /// <summary>
        /// The top-left position of the bounds
        /// </summary>
        public Vector Position { get { return position; } }

        /// <summary>
        /// The width and height of the bounds, in terms of X and Y 
        /// </summary>
        public Vector Size { get { return size; } }

        /// <summary>
        /// Tthe bottam Y location of the bound
        /// </summary>
        public float Bottam { get { return position.Y + size.Y; } }

        /// <summary>
        /// Tthe top Y location of the bound
        /// </summary>
        public float Top { get { return position.Y; } }

        /// <summary>
        /// Tthe right X location of the bound
        /// </summary>
        public float Right { get { return position.X + size.X; } }

        /// <summary>
        /// Tthe left X location of the bound
        /// </summary>
        public float Left { get { return position.X; } }

        public Vector TopLeft { get { return Position; } }
        public Vector TopRight { get { return TopLeft + Vector.UnitX * Size.X; } }
        public Vector BottomLeft { get { return TopLeft + Vector.UnitY * Size.Y; } }
        public Vector BottomRight { get { return TopLeft + Size; } }



        /// <summary>
        /// Create a Bound from a top-left position, and a width-height
        /// </summary>
        /// <param name="position">The top-left position of the bounds</param>
        /// <param name="size">The width-height of the bounds, in terms of X and Y</param>
        public Bounds(Vector position, Vector size)
        {
            this.position = position;
            this.size = size;
        }

        /// <summary>
        /// Determines if a given vector is within the bounds
        /// </summary>
        /// <param name="vec">some vector to check</param>
        /// <returns>true if the point is within the bounds, false otherwise</returns>
        public virtual Boolean vectorWithin(Vector vec)
        {

            return vec.X > position.X
                && vec.X < position.X + size.X
                && vec.Y > position.Y
                && vec.Y < position.Y + size.Y;
                
        }


        public virtual Boolean boundWithin(Bounds bound)
        {

            return _inBound(this, bound) || _inBound(bound, this);

        }

        private static Boolean _inBound(Bounds a, Bounds b)
        {
            return (a.vectorWithin(b.TopLeft) ||
                    a.vectorWithin(b.TopRight) ||
                    a.vectorWithin(b.BottomLeft) ||
                    a.vectorWithin(b.BottomRight));

        }
    }
}
