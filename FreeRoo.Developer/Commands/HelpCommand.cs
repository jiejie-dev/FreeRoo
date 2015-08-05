using System;
using System.Linq;

namespace FreeRoo.Developer
{
	[Command("the freeroo developer command list ","help","no args")]
	public class HelpCommand:ICommand
	{
		private ICmdContext _context;
		public HelpCommand (ICmdContext context)
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
			Console.WriteLine ("command list :");
			var types = _context.GetCmdContainer ().GetAllCmdNameList ()
				.Where (item => item != "help" && item != "unknow" && item != "i");
			foreach (var item in types) {
				Console.WriteLine (item.Replace ("Command", "").ToLower ());
			}
		}
	}
}

