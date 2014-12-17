﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whiskey2D.Core;
using WhiskeyEditor.Backend;
using WhiskeyEditor.MonoHelp;
using WhiskeyEditor.Backend.Managers;

namespace WhiskeyEditor.EditorObjects
{
    [Serializable]
    class SelectScript : Script<ObjectController>
    {


        public override void onStart()
        {

            SelectionManager.Instance.SelectedInstanceChanged += (s, a) =>
            {
                Gob.Selected = SelectionManager.Instance.SelectedInstance;
                if (Gob.Selected == null)
                {
                    Gob.Unselect = true;

                }
            };

        }

        public override void onUpdate()
        {
            if (WhiskeyControl.InputManager.isNewMouseDown(Whiskey2D.Core.Inputs.MouseButtons.Left))
            {
                Gob.Selected = null;
                Gob.Sprite.Color = Color.Transparent;
                //GameManager.Controller.SelectedGob = null;
                List<InstanceDescriptor> objs = Gob.CurrentLevel.getInstances();//GameManager.Objects.getAllObjectsNotOfType<EditorGameObject>();

                foreach (InstanceDescriptor obj in objs)
                {
                    Vector v = WhiskeyControl.InputManager.getMousePosition();
                    if (obj.Sprite != null && obj.Bounds.vectorWithin(WhiskeyControl.InputManager.getMousePosition()) || new Bounds(obj.Position - obj.Sprite.Offset - Vector.One * 8, Vector.One * 16).vectorWithin(WhiskeyControl.InputManager.getMousePosition()))
                    {
                        Gob.Selected = obj;

                        SelectionManager.Instance.SelectedInstance = obj;
                        

                        break;
                    }

                }

                if (Gob.Selected == null)
                {
                    SelectionManager.Instance.SelectedInstance = null;
                }

            }

            

            if (Gob.Selected != null)
            {

                Gob.Sprite.Color = Color.Orange;
                Gob.Position = Gob.Selected.Bounds.Position - new Vector(5, 5);
                Gob.Sprite.Scale = Gob.Selected.Bounds.Size + new Vector(10, 10);
            }

            
            else if (Gob.Unselect)
            {
                Gob.Unselect = false;
                Gob.Sprite.Color = Color.Transparent;
            }

        }
    }
}
