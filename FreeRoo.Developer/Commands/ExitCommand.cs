using System;

namespace FreeRoo.Developer
{
	[Command("exit the freeroo developer ","exit","no args")]
	public class ExitCommand:ICommand
	{
		ICmdContext _context;
		public ExitCommand (ICmdContext context)
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
			Console.WriteLine ("bye");
			_context.GetDevelopLooper ().ExitLoop ();
		}
	}
}

