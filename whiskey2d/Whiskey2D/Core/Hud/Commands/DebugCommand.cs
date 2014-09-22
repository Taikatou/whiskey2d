﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whiskey2D.Core.Hud.Commands
{
    class DebugCommand : ConsoleCommand
    {
        public DebugCommand() : base("debug") { }

        public override void run(WhiskeyConsole console, string[] args)
        {

            if (args.Length == 2)
            {
                string arg = args[1];

                arg = arg.ToLower();

                if (arg.Equals("0") || arg.Equals("n") || arg.Equals("off") || arg.Equals("no"))
                    HudManager.getInstance().DebugVisible = false;
                else if (arg.Equals("1") || arg.Equals("y") || arg.Equals("yes") || arg.Equals("on"))
                    HudManager.getInstance().DebugVisible = false;
            }

            

        }
    }
}
