﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using WhiskeyEditor.MonoHelp;
using Microsoft.Xna.Framework.Input;


namespace WhiskeyEditor.EditorObjects
{
    class CameraMovementScript : Script<ObjectController>
    {
        Vector mouseShiftStart = Vector.Zero;
        Vector mouseShiftCameraStart = Vector.Zero;
        public override void onStart()
        {
        }

        public override void onUpdate()
        {
            Camera camera = WhiskeyControl.ActiveCamera;
            
            Vector cameraVel = Vector.Zero;
            if (WhiskeyControl.InputManager.isKeyDown(Keys.Left))
            {
                cameraVel.X -= 2;// camera.getCameraUnits(2);
            }
            if (WhiskeyControl.InputManager.isKeyDown(Keys.Right))
            {
                cameraVel.X += 2;// camera.getCameraUnits(2);
            }

            if (WhiskeyControl.InputManager.isKeyDown(Keys.Up))
            {
                cameraVel.Y -= 2;
            }
            if (WhiskeyControl.InputManager.isKeyDown(Keys.Down))
            {
                cameraVel.Y += 2;
            }

            camera.Position -= cameraVel;
            camera.PositionSpring = 1;
            camera.PositionFriction = 0;
            camera.OriginSpring = 1;
            camera.OriginFriction = 0;
            camera.ZoomSpring = 1;
            camera.ZoomFriction = 0;
            if (Gob.Selected == null && WhiskeyControl.InputManager.isMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
            {

                if (WhiskeyControl.InputManager.isNewMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
                {
                    mouseShiftStart = WhiskeyControl.InputManager.getMousePosition();
                    mouseShiftCameraStart = camera.Position;
                }

                Vector mouseDelta = WhiskeyControl.InputManager.getMousePosition() - mouseShiftStart;
               // camera.Origin = mouseShiftCameraStart + mouseDelta;
                camera.TargetPosition = mouseShiftCameraStart + mouseDelta;
                camera.PositionSpring = mouseDelta.Length;
                
                

               // camera.follow(camera.getGameCoordinate(mouseShiftCameraStart + mouseDelta));

            }
            else
            {
                camera.Origin = WhiskeyControl.InputManager.getMousePosition();
            }
           // camera.setOriginLockPosition(WhiskeyControl.InputManager.getMousePosition());


            //camera.Origin = WhiskeyControl.InputManager.getMousePosition();
            if (WhiskeyControl.InputManager.scrolledDown())
            {
                //camera.Origin = origin;
                camera.TargetZoom -= .1f;
            }
            if (WhiskeyControl.InputManager.scrolledUp())
            {
                //camera.Origin = origin;
                camera.TargetZoom += .1f;
            }

            //camera.Zoom = Math.Max(camera.Zoom, .5f);
            //camera.Zoom = Math.Min(camera.Zoom, 1.5f);

            camera.update();
            
        }

        public override void onClose()
        {
        }
    }
}
