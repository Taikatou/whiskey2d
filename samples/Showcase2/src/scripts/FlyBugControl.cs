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
	public class FlyBugControl : Script<FlyBug>
	{
	
		SimpleObject vis;
		Animation idle;
		Animation attack;
	
		GameObject target;
		bool attacking = false;
	
		public override void onStart()
		{

			target = Objects.getObject(Gob.Target);
			Gob.Sprite.Color = new Color(0, 1, 0, .2f);
			Gob.Sprite.Visible = false;
			vis = new SimpleObject(Level);
			vis.Sprite.ImagePath = "enemy3.png";
			vis.Sprite.Rows = 1;
			vis.Sprite.Columns = 31;
			vis.Position = Gob.Position;
			vis.Sprite.Scale = Gob.Sprite.Scale / 300f;
			idle = vis.Sprite.createAnimation(13, 19, 2, true);	
			attack = vis.Sprite.createAnimation(1, 6, 4, true);
		}
		
		public override void onUpdate() 
		{

			if (target != null){
			
				Vector toTarget = target.Position - Gob.Position;
				Vector toTargetUnit = toTarget.UnitSafe;
				
				int dir = Math.Sign(toTarget.X);
				//attacking = false;
				if (toTarget.Length < Gob.HuntRadius){
				
					if (Math.Abs(toTarget.X) < 100 && Math.Abs(toTarget.Y) < 50){
						attacking = true;
					//	Gob.Acceleration -= toTargetUnit * .1f;
					} else if (!attacking && (Math.Abs(toTarget.X) > 120 || Math.Abs(toTarget.Y) > 50)){
						
						Gob.Acceleration += toTargetUnit * 1f;
					}
				}
				
				
				foreach (var o in Objects.getAllObjectsOfType<FlyBug>()){
					if (o != Gob){
						Vector toOther = o.Position - Gob.Position;
						if (toOther.Length < 150){
							Gob.Acceleration -= toOther.UnitSafe;
						}
					
					}
				}
				
				Gob.Velocity += Gob.Acceleration;
				Gob.Velocity -= Gob.Velocity * .1f;
			
				Gob.Position += Gob.Velocity;
				
//				var colls = Gob.currentCollisions<Wall>();
//				foreach (var c in colls){
//					Gob.Position -= c.MTV;
//					Gob.Velocity = c.Normal.Perpendicular * c.Normal.Perpendicular.dot(Gob.Velocity);
//					//Gob.Velocity += c.Normal.Perpendicular;
//				}
				
				
			
				Gob.Acceleration = Vector.Zero;
				vis.Sprite.Scale = new Vector(dir * Math.Abs(vis.Sprite.Scale.X), vis.Sprite.Scale.Y);
			}

			vis.Position = Gob.Position;
			
			
			if (attacking){
				attack.advanceFrame();
				
				if (attack.CurrentFrame == 5){
					attacking = false;
				}
				
			} else {
				idle.advanceFrame();
			}
			
			
			
		}
		
		public override void onClose()  
		{
		 
		 	float x = Math.Sign(vis.Sprite.Scale.X);
		 	SpriteEffect boom = new SpriteEffect(Level);
		 
		 	boom.Effect = "bigBang";
		 	boom.Frames = new Vector(1, 48);
		 	boom.Position = Gob.Position;
		 	boom.Speed = 1;
		 	boom.Sprite.Scale *= 1.5f;
		 	
		 	vis.close();
		 	
		 	SpriteEffect death = new SpriteEffect(Level);
		 
		 	death.Effect = "enemy3";
		 	death.Frames = new Vector(1, 31);
		 	death.Position = Gob.Position+ new Vector(0, -75);;
		 	death.Speed = 13;
		 	death.StartFrame = 8;
		 	death.EndFrame = 11;
		 	death.Sprite.Scale = new Vector(x * Gob.Sprite.Scale.X, Gob.Sprite.Scale.Y) / 300f;
		 	
		 
		 
		}
		
	}
}




















































