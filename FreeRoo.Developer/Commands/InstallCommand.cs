using System;

namespace FreeRoo.Developer
{
	[Command("install module into the freeroo developer ","exit","no args")]
	public class InstallCommand:ICommand
	{
		ICmdContext _context;
		public InstallCommand (ICmdContext context)
		{
			_context = context;
		}

		private string[] _args;

		public void SetArgs (string[] args)
		{
			this._args = args;
		}

		public void Excute ()
		{
			Console.WriteLine ("install error not impliment");
		}
	}
}

