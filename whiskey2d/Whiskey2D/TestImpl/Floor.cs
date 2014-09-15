﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Whiskey2D.Core;

namespace Whiskey2D.TestImpl
{
    class Floor : GameObject
    {

        public Floor() 
        {
            Sprite = new Sprite(RenderManager.getInstance().getPixel());
            Size = new Vector2(20, 20);
           
        }

        private Vector2 size;

        public Vector2 Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                Sprite.Scale = value;
            }
        }

        protected override List<Script> getInitialScripts()
        {
            return null;
        }
    }
}