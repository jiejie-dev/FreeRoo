using System;

namespace FreeRoo.Developer
{
	public enum CommandLineType
	{
		Single = 0,
		Multi = 1
	}

	public class CommandLine
	{
		public CommandLine ()
		{
		}

		public string Content{ get; set; }

		public static CommandLine New ()
		{
			Console.Write (ConsoleTemplate.CmdHead);
			return new CommandLine ();
		}
		public static CommandLine Empty()
		{
			Console.WriteLine (ConsoleTemplate.EmptyLine);
			return new CommandLine ();
		}
		public static CommandLine Hyphen()
		{
			Console.WriteLine (ConsoleTemplate.HyphenLine);
			return new CommandLine ();
		}
	}
}

