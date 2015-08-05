using System;

namespace FreeRoo.Developer
{
	[Command("package module for the freeroo developer ","exit","no args")]
	public class PackageCommand:ICommand
	{
		ICmdContext _context;
		public PackageCommand (ICmdContext context)
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
			Console.WriteLine ("package error not impliment");
		}
	}
}

