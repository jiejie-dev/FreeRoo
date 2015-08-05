using System;

namespace FreeRoo.Developer
{
	[Command("command not found ","unknow","no args")]
	public class UnKnowCommand:ICommand
	{
		private ICmdContext _context;
		public UnKnowCommand (ICmdContext context)
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
			Console.WriteLine ("unknow command !");
		}
	}
}

