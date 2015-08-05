using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace FreeRoo.Developer
{
	public class CommandParser:ICommandParser
	{
		private ICmdContainer _cmdContainer;
		public CommandParser()
		{
			this._cmdContainer = new CmdContainer ();
		}
		public ICommand Parser(string cmdLine)
		{
			ICmdContext context = new CmdContext ();

			var arrs = cmdLine.Split (' ');
			var cmds = _cmdContainer.GetAllCmdTypes ();
			var cmdType = cmds.FirstOrDefault (item => item.Name.Replace ("Command","").ToLower () == arrs [0]);
			ICommand cmd;
			if (cmdType != default(Type)) {
				cmd = Activator.CreateInstance (cmdType, new object[]{ context }) as ICommand;
			}else if(arrs[0] == "?"){
				cmd = new HelpCommand (context);
			}else {
				cmd = new UnKnowCommand (context);
			}
			return cmd;
		}
	}
}

