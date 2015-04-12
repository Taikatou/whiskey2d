using System;
using System.Collections.Generic;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using Whiskey2D.Core.Inputs;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

//auto-generated by Whiskey2D
namespace Project
{
	[Serializable] 
	public class WormControl : Script<Worm>
	{
	
		Animation idle;
		GameObject target;
	
		Vector scale;
	
		public override void onStart()
		{
			scale = Gob.Sprite.Scale;
			target = Objects.getObject(Gob.Target);
			idle = Gob.Sprite.createAnimation(16, 20, 7, true);
			
		}
		
		public override void onUpdate() 
		{

			float toTarget = Math.Sign(target.X - Gob.X);
			
			Gob.Sprite.Scale = new Vector(toTarget * scale.X, scale.Y);
			idle.advanceFrame();
		}
		
		public override void onClose()  
		{
		 	float x = Math.Sign(Gob.Sprite.Scale.X);
		 	SpriteEffect boom = new SpriteEffect(Level);
		 
		 	boom.Effect = "bigBang";
		 	boom.Frames = new Vector(1, 48);
		 	boom.Position = Gob.Position;
		 	boom.Speed = 1;
		 	boom.Sprite.Scale *= 1.5f;
		 	
		 
		 	SpriteEffect death = new SpriteEffect(Level);
		 
		 	death.Effect = "enemy2c";
		 	death.Frames = new Vector(2, 16);
		 	death.Speed = 13;
		 	death.StartFrame = 10;
		 	death.EndFrame = 16;
		 	death.Sprite.Scale = new Vector(x, 1) * 1.5f;
		}
		
	}
}














