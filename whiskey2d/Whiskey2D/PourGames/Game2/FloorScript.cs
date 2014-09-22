﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Whiskey2D.Core;
using Microsoft.Xna.Framework;


namespace Whiskey2D.PourGames.Game2
{
    class FloorScript : Script<Floor>
    {
        

        public override void onStart()
        {
            //throw new NotImplementedException();
        }

        public override void onUpdate()
        {
            Floor floor = (Floor)this.Gob;

            float speed = ObjectManager.getInstance().getAllObjectsOfType<GameControl>()[0].gameSpeed;
            floor.Position.X -= speed;


            if (floor.Bounds.Right < 0)
            {
                floor.close();
            }

        }
    }
}