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
	public class TracerFade : Script<Tracer>
	{
		public override void onStart()
		{
			Gob.Light.Visible = true;
		}
		
		public override void onUpdate() 
		{
	 		Color c = Gob.Sprite.Color;
	 		c.A = (c.A * Gob.Decay) /(Gob.Decay + 1);
	 		Gob.Sprite.Color = c;
	 		Gob.Y -= .3f;
	 		if (c.A < 1){
	 			Gob.close();
	 		}
	 		
	 		Gob.Position += Gob.Dir * Gob.Speed;
	 		
		}
		
		public override void onClose()  
		{
		 //This code runs when the GameObject is closed
		}
		
	}
}










