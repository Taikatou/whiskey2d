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
	public class ButtonControl : Script<Button>
	{
	
		Player plr;
		Script scr;
		
		
		public override void onStart()
		{
			 plr = Objects.getObject<Player>("Player1");
			 scr = Gob.Scripts[1];
			 
			 
		}
		
		public override void onUpdate() 
		{
			
			if (plr != null){
				if (plr.Bounds.boundWithin(Gob.Bounds)){
				
					if (Input.isNewKeyDown(Keys.Enter)){
						
						Gob.Pressed = !Gob.Pressed;
						
						if (Gob.Pressed){
							
							scr.Active = true;
						}
						
					}
				
				}
			}
		 	
		 	
		 	if (Gob.Pressed){
		 		Gob.Sprite.Color = Color.LimeGreen;
		 	} else {
		 		Gob.Sprite.Color = Color.Tomato;
		 	}
		 	
		 
		}
		
		public override void onClose()  
		{
		 //This code runs when the GameObject is closed
		}
		
	}
}





















