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
	public class CameraFollow : Script<Player>
	{
	
		CameraZone lastZone = null;
		bool lastZoneFlag = false;
	
		public override void onStart()
		{

			Level.Camera.Zoom = 1f;
			Level.Camera.PositionSpeed = 20f;
			Level.Camera.ZoomSpring = .01f;
			Level.Camera.ZoomFriction = .1f;
			
			
		}
		
		public override void onUpdate() 
		{

			var zones = Gob.currentCollisions<CameraZone>();
			float l = 0, r = 0, t = 0, b = 0, z = 0;
			
			if (zones.Count == 1){
				lastZone = zones[0].Gob;
				lastZoneFlag = false;
				
				
				
			} else if (zones.Count == 2){
				
				
				
				if (!lastZoneFlag){
					lastZoneFlag = true;
					
					if (zones[1].Gob == lastZone)
						lastZone = zones[0].Gob;
					else lastZone = zones[1].Gob;
					
				}
			
			}
			l = lastZone.Bounds.Left;
			r = lastZone.Bounds.Right;
			t = lastZone.Bounds.Top;
			b = lastZone.Bounds.Bottom;
			z = lastZone.Zoom;
			
			
//			foreach (var c in zones){
//				CameraZone zone = c.Gob;
//				l = zone.Bounds.Left;
//				r = zone.Bounds.Right;
//				t = zone.Bounds.Top;
//				b = zone.Bounds.Bottam;
//				z = zone.Zoom;
//			}
//			
//			r /= zones.Count;
//			l /= zones.Count;
//			t /= zones.Count;
//			b /= zones.Count;
//			z /= zones.Count;
//			
			Level.Camera.TargetZoom = z;
//			Log.debug("t zoom = " + z);
//			Log.debug("c zoom = " + Level.Camera.Zoom);
			Level.Camera.followClamped(Gob, l, t, r, b);
			
		}
		
		public override void onClose()  
		{
		 //This code runs when the GameObject is closed
		}
		
	}
}





















































