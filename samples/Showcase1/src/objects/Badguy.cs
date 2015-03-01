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
	public class Badguy : GameObject
	{
		#region AUTO-GENERATED BY WHISKEY2D. DO NOT EDIT. (please)
		public Vector Velocity { get; set; } 

		#region INIT_VALUE_ASSIGNMENT
		protected override void initProperties()
		{
			X = 0f;
			Y = 0f;
			Sprite = new Sprite("__PIXEL__", new Vector(100f, 100f), new Vector(0f, 0f), 0.5f, new Color(255, 0, 0, 255), 0f);
			Light = new Light(new Vector(0f, 0f), new Color(255, 255, 255, 255), 512f, false);
			Shadows = new ShadowProperties(false, true, false, 1f, 100f);
			Name = "???";
			Active = true;
			HudObject = false;
			IsDebug = false;
			Velocity = new Vector(0f, 0f);
		}
		#endregion

		public Badguy( ObjectManager objMan ) : base (objMan) { }
		#region INIT_SCRIPTS
		protected override void addInitialScripts()
		{
		}
		#endregion

		#endregion END OF AUTO-GENERATED CODE.

		public override void initializeObject()
		{
			//implement your code here!
		}

	}
}
