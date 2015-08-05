using System;

namespace FreeRoo.Developer
{
	public class DevelopLooper:IDevelopLooper
	{
		public static DevelopLooper Current;

		private bool _loop;
		private ICommandParser _cmdParser;

		public DevelopLooper ()
		{
			this._loop = true;
			this._cmdParser = new CommandParser ();
		}

		public void StartLoop ()
		{
			this._loop = true;

			Console.WriteLine ("Welcome to FreeRoo Developer Console !");
			CommandLine.Hyphen ();
			CommandLine.New ();
			while (_loop) {
				var line = Console.ReadLine ();
				if (string.IsNullOrEmpty (line))
					continue;
				if (line.IndexOf ("\t") > -1) {
					line = line.Replace ("\t", "");
					Console.WriteLine (line);
				}
				var cmd = _cmdParser.Parser (line);
				CommandLine.New ();
				cmd.Excute ();
				if (_loop)
					CommandLine.New ();
			}

		}

		public void ExitLoop ()
		{
			_loop = false;
		}
	}
}

