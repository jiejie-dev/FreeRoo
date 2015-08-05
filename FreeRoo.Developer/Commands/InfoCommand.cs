using System;

namespace FreeRoo.Developer
{
	[Command("the freeroo developer info","info","no args")]
	public class InfoCommand:ICommand
	{
		private ICmdContext _context;
		public InfoCommand (ICmdContext context)
		{
			_context = context;
		}

		private string[] _args;
		public void SetArgs(string[] args)
		{
			this._args = args;
		}
		public void Excute()
		{
			Console.WriteLine ("FreeRoo Developer 2015 Copyright");
			Console.WriteLine ("Version : 0.0.1");
			Console.WriteLine ("Home Page : http://lujiejie.com");
		}
	}
}

