﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Whiskey2D.Core.Managers;
using Whiskey2D.Core;
using WhiskeyEditor.Backend;

namespace WhiskeyEditor.MonoHelp
{
    using XnaColor = Microsoft.Xna.Framework.Color;

    public class EditorRenderManager : RenderManager
    {

        public GraphicsDevice GraphicsDevice { get; private set; }
        private SpriteBatch spriteBatch;
        private static Texture2D pixel;

        private Sprite alwaysOnSprite;

        //public Camera Camera { get; set; }

        /// <summary>
        /// Initializes the RenderManager
        /// </summary>
        public void init(GraphicsDevice graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice;
            this.spriteBatch = new SpriteBatch(this.GraphicsDevice);
          //  Camera = new Camera();
            ActiveCamera = new Camera();
            alwaysOnSprite = new Sprite("selection.png", Vector.One, Vector.Zero, .5f, XnaColor.White, 0);
            alwaysOnSprite = new Sprite(WhiskeyControl.Renderer, WhiskeyControl.Resources, alwaysOnSprite);
            alwaysOnSprite.getImage();
            alwaysOnSprite.Center();
            alwaysOnSprite.Scale *= .5f;
        }

        /// <summary>
        /// Closes out the RenderManager
        /// </summary>
        public void close()
        {
            this.spriteBatch.Dispose();
        }

        /// <summary>
        /// Renders the Game
        /// </summary>
        public void render(List<GameObject> descs)
        {

            Matrix transform = ActiveCamera != null ? ActiveCamera.TranformMatrix : Matrix.Identity;


            //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);


            //    //draw grid
            //    if (ActiveCamera != null)
            //    {


            //        Vector topLeft =  (Vector.Zero);
            //        Vector botRight =  (new Vector(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight));

            //        int gridSize = 100;
            //        drawLine(spriteBatch, new Vector(topLeft.X, -ActiveCamera.Position.Y + 100) * ActiveCamera.Zoom, new Vector(botRight.X, -ActiveCamera.Position.Y + 100) * ActiveCamera.Zoom);
            //        //float currY = topLeft.Y - (topLeft.Y % gridSize);
            //        //while (currY < botRight.Y)
            //        //{
            //        //    drawLine(spriteBatch, new Vector(topLeft.X, currY), new Vector(botRight.X, currY));

            //        //    currY += gridSize;
            //        //}

                    


            //    }

            //spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, transform);
            
            foreach (GameObject gob in descs)
            {
                Sprite spr = gob.Sprite;
                if (spr != null)
                {
                    spriteBatch.Draw(spr.getImage(), gob.Position, null, spr.Color, spr.Rotation, spr.Offset, spr.Scale, SpriteEffects.None, spr.Depth/2);
                }
            }

            
            if (ActiveCamera != null)
            {

                
                Vector topLeft = ActiveCamera.getGameCoordinate(Vector.Zero);
                Vector botRight = ActiveCamera.getGameCoordinate(new Vector(GraphicsDevice.PresentationParameters.BackBufferWidth, GraphicsDevice.PresentationParameters.BackBufferHeight));


                //draw grid
                int gridSize = 120;

                float currY = topLeft.Y - (topLeft.Y % gridSize);
                while (currY < botRight.Y)
                {

                    XnaColor color = XnaColor.White;
                    if (currY == 0)
                        color = XnaColor.Indigo;
                   
                    drawLine(spriteBatch, color, .01f, new Vector(topLeft.X, currY), new Vector(botRight.X, currY));
                    spriteBatch.DrawString(WhiskeyControl.Resources.getDefaultFont(), "" + currY, new Vector2(topLeft.X, currY - WhiskeyControl.Resources.getDefaultFont().MeasureString("A").Y), XnaColor.Crimson, 0, Vector2.Zero, 1, SpriteEffects.None, .04f);
                    
                    currY += gridSize;
                }

                float currX = topLeft.X - (topLeft.X % gridSize);
                while (currX < botRight.X)
                {
                    XnaColor color = XnaColor.White;
                    if (currX == 0)
                        color = XnaColor.Indigo;
                    drawLine(spriteBatch, color, .01f, new Vector(currX, topLeft.Y), new Vector(currX, botRight.Y));
                    spriteBatch.DrawString(WhiskeyControl.Resources.getDefaultFont(), "" + currX, new Vector2(currX - WhiskeyControl.Resources.getDefaultFont().MeasureString("A").X, topLeft.Y), XnaColor.Crimson, 0, Vector2.Zero, 1, SpriteEffects.None, .04f);
                    currX += gridSize;
                }


                //draw screen-box
                int screenWidth = 1280;
                int screenHeight = 720;
                Vector screenSize = new Vector(screenWidth, screenHeight);
                Vector center = (topLeft + botRight) / 2;

                Vector screenTopLeft = center - screenSize / 2;
                Vector screenBotRight = center + screenSize / 2;


                drawBox(spriteBatch, XnaColor.Blue, .02f, screenTopLeft, screenBotRight);
                drawBox(spriteBatch, XnaColor.Red, .03f, Vector.Zero, Vector.Zero + screenSize );

            }



            spriteBatch.End();
        }

        private void drawLine(SpriteBatch spriteBatch, XnaColor color, float depth, Vector start, Vector end)
        {
            Vector diff = end - start;

            spriteBatch.Draw(getPixel(), start, null, color, (float)Math.Atan2(diff.Y, diff.X), new Vector2(0, .5f) , new Vector(diff.Length(), 2), SpriteEffects.None, depth);
        }

        private void drawBox(SpriteBatch spriteBatch, XnaColor color, float depth, Vector topLeft, Vector botRight)
        {
            drawLine(spriteBatch, color, depth, topLeft, new Vector(botRight.X, topLeft.Y));
            drawLine(spriteBatch, color, depth, topLeft, new Vector(topLeft.X, botRight.Y));
            drawLine(spriteBatch, color, depth, botRight, new Vector(topLeft.X, botRight.Y)); //bottom line
            drawLine(spriteBatch, color, depth, botRight, new Vector(botRight.X, topLeft.Y));
        }


        public void render(List<InstanceDescriptor> descs)
        {
            Matrix transform = ActiveCamera != null ? ActiveCamera.TranformMatrix : Matrix.Identity;

            //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, transform);
            foreach (InstanceDescriptor gob in descs)
            {
                Sprite spr = gob.Sprite;
                spr.setRender(this);
                spr.setResources(WhiskeyControl.Resources);
                if (spr != null)
                {
                    Vector2 alwaysOnPos = gob.Position;
                    spriteBatch.Draw(alwaysOnSprite.getImage(), alwaysOnPos, null, alwaysOnSprite.Color, alwaysOnSprite.Rotation, alwaysOnSprite.Offset, alwaysOnSprite.Scale, SpriteEffects.None, (spr.Depth / 2) - .01f);
                    spriteBatch.Draw(spr.getImage(), gob.Position, null, spr.Color, spr.Rotation, spr.Offset, spr.Scale, SpriteEffects.None, spr.Depth / 2);
                }
            }

            spriteBatch.End();
        }

        /// <summary>
        /// Renders the HUD
        /// </summary>
        public void renderHud()
        {
            //TODO fix this
        }


        /// <summary>
        /// Gets a pixel image
        /// </summary>
        /// <returns></returns>
        public Texture2D getPixel()
        {
            if (pixel == null)
            {
                pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                pixel.SetData<XnaColor>(new XnaColor[] { XnaColor.White });
            }
            return pixel;
        }




        public void render()
        {
            //do nothing
        }


        public Camera ActiveCamera
        {
            get { return WhiskeyControl.ActiveCamera; }
            set { }
        }

        
    }
}
