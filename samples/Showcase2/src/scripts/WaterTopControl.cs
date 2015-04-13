using System;
using System.Collections.Generic;
using Whiskey2D.Core;
using Whiskey2D.Core.Managers;
using Whiskey2D.Core.Inputs;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

//auto-generated by Whiskey2D
namespace Project
{
	[Serializable] 
	public class WaterTopControl : Script<WaterBlock>
	{
	
		List<WaterNode> nodes;
	
		public override void onStart()
		{

			nodes = new List<WaterNode>();
			int index = 0;
			for (float x = (float) Gob.Bounds.Left; x < (float) Gob.Bounds.Right + 46f; x += 45f){
				WaterNode node = new WaterNode(Level, new Vector(x, Gob.Bounds.Top));
				node.Floor = Gob.Bounds.Bottom;
				node.Layer = Gob.Layer;
				if (index == 0){
					node.Wave = true;
				}
				nodes.Add(node);
				
				if (index > 0){
					node.LeftNode = nodes[index - 1];
					nodes[index-1].RightNode = node;
				}
				node.Name = "WaterNode" + index;
				
				index ++;
			}
			
			
			
			
//			WaterNode node2 = new WaterNode(Level, new Vector(Gob.Bounds.Right, Gob.Bounds.Top));
//			node2.Floor = Gob.Bounds.Bottom;
//			if (index > 0){
//				node2.LeftNode = nodes[index - 1];
//				nodes[index-1].RightNode = node2;
//			}
//			nodes.Add(node2);
//			node2.Name = "WaterNode" + index;
			

		}
		
		public override void onUpdate() 
		{
		 	int hitBottom = 0;
		 	float speed = 3f;
		 	nodes.ForEach( n=> {
		 	
		 		n.Velocity += -.01f * n.Velocity;
		 	
		 		Vector fUs = n.KValue * (n.RestPosition - n.Position);
		 		n.Acceleration += fUs;
		 		
		 		if (n.LeftNode != null){
		 			Vector fLeft = 110 * -n.KValue * ((n.LeftNode.RestPosition - n.RestPosition) - (n.LeftNode.Position - n.Position));
		 			
		 			n.Acceleration += fLeft / 2;
		 			n.LeftNode.Acceleration += fLeft / -2;
		 		
		 		}
		 		
		 		if (n.RightNode != null){
		 			Vector fRight = 110 * -n.KValue * ((n.RightNode.RestPosition - n.RestPosition) - (n.RightNode.Position - n.Position));
		 			
		 			n.Acceleration += fRight /2;
		 			n.RightNode.Acceleration += fRight / -2;
		 		}
		 		
		 		
		 		if (Gob.Sink){
		 			if (n.RestPosition.Y <= Gob.Bounds.Bottom){
		 				
		 				n.RestPosition += new Vector(0, speed);
		 				
		 			} else {
		 				hitBottom ++;
		 			}
		 		
		 		}
		 		
		 	});
		 	
		 	if (Gob.Sink){
		 		Gob.Sprite.Scale = new Vector(Gob.Sprite.Scale.X, Gob.Sprite.Scale.Y - (speed / Math.Abs(Gob.Sprite.FrameHeight)));
		 		Gob.Y += speed;
		 	}
		 	
		 	if (Gob.Sprite.Scale.Y < .01f){
		 		nodes.ForEach(n => n.close());
		 		Gob.Sink = false;
		 		Gob.Active = false;
		 	
		 	}
		 	
		 
		}
		
		public override void onClose()  
		{
		 //This code runs when the GameObject is closed
		}
		
	}
	
	
	[Serializable]
	public class WaterNode : GameObject{
	
	
		public Vector RestPosition {get; set;} 
		public float KValue {get; set;}
		public WaterNode LeftNode {get; set;}
		public WaterNode RightNode {get; set;}
		public Vector Acceleration {get; set;}
		public Vector Velocity {get; set; }
		public float Floor {get; set;}
		public bool Wave {get; set;}
		
		public WaterNode(GameLevel l, Vector pos) : base(l)
		{
			Position = pos;
			initializeObject();
		}
		
		protected override void addInitialScripts()
		{
			base.addScript( new WaterNodeControl() );
		}
		
		public override void initializeObject()
		{
			Sprite.Color = Color.Green;
			Sprite.Scale = Vector.One * .08f;
			Sprite.Depth = .7f;
			//Sprite.ImagePath = "gradient2";
			KValue = .005f;
			RestPosition = Position;
			Acceleration = Vector.Zero;
			Velocity = Vector.Zero;
			
		}
		
		
		public override void renderImage(RenderInfo info){
		
			
			if (LeftNode != null){
				Convex c = new Convex(Vector.Zero, 0, new VectorSet(
					LeftNode.Position,
					Position,
					new Vector(Position.X, Floor),
					new Vector(LeftNode.Position.X, Floor)));
				Color waterColor = new Color(.3f, .05f, .5f, 1);
				c.renderWhenReady(info.GraphicsDevice, info.Transform, new RenderHints().setColor(waterColor).setPrimitiveType(PrimitiveType.TriangleStrip));
				//	GameManager.Log.debug("Hello from renderer");
			}	
			//
			//base.renderImage(info);
		}
		
	
	}
	
	[Serializable] 
	public class WaterNodeControl : Script<WaterNode>
	{
		float waveAmplitude = 3;
		float waveFrequency = 2;
		float waveActuator = 0;
		WaterParticle genWp = null;
	
		public override void onStart() {
		
			//waveActuator = Objects.getAllObjectsOfType<WaterNode>().Count * .01f * (float)(Math.PI/2);
		}
		public override void onUpdate() {
		
	
		
			
		
			if (Gob.Wave == true){
				//Gob.Velocity += new Vector(0, waveAmplitude * (float)Math.Sin(waveFrequency * waveActuator));
				waveActuator += .01f;
			}
		
		
				bool hasNeighbors = (Gob.RightNode != null && Gob.LeftNode != null);
		
		
				
		
				Gob.Velocity += new Vector(hasNeighbors ? Gob.Acceleration.X : 0, Gob.Acceleration.Y);
				
				if (Gob.Velocity.Length > 60){
					Gob.Velocity = 60 * Gob.Velocity.Unit;
				}
				
				Gob.Position += Gob.Velocity;
				Gob.Acceleration = Vector.Zero;
			
			//} 
		}
		public override void onClose() {}
		
	
	}
}





































































