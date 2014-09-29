﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Whiskey2D.Core.Managers
{
    public interface RenderManager
    {

        /// <summary>
        /// Initializes the RenderManager
        /// </summary>
        void init(GraphicsDevice graphicsDevice);

        /// <summary>
        /// Closes out the RenderManager
        /// </summary>
        void close();

        /// <summary>
        /// Renders the Game
        /// </summary>
        void render();


        /// <summary>
        /// Renders the HUD
        /// </summary>
        void renderHud();


        /// <summary>
        /// Gets a pixel image
        /// </summary>
        /// <returns></returns>
        Texture2D getPixel();

    }
}