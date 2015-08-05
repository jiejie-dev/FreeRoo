using System;
using System.Linq;

namespace FreeRoo.Developer
{
	public class TabCompleteWorker
	{
		private ICmdContext _context;
		public TabCompleteWorker (ICmdContext context)
		{
			_context = context;
		}
		public string GetCompleteCmd(string str)
		{
			var resultType = _context.GetCmdContainer ().GetAllCmdNameList ()
				.FirstOrDefault (item => item.StartsWith (str));
			if (!string.IsNullOrEmpty (resultType)) {
				return resultType.ToLower ();
			} else {
				return "command not found !";
			}
		}
	}
}

