using System;
using FreeRoo.Framework;
using System.IO;
using System.Diagnostics;

namespace CompileTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("start test");
			CompileEngine engine = new CompileEngine ();
			var result = engine.CompileProject (Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "Project", "TestDemo.csproj"));
			Console.WriteLine ("has errors : " + result.Errors.HasErrors);
			Console.WriteLine ("has warnings : " + result.Errors.HasWarnings);
			if (result.Errors.HasErrors) {
				foreach (var item in result.Errors) {
					Console.WriteLine (item);
				}
			}
			ProcessStartInfo startInfo = new ProcessStartInfo ();
			startInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
			startInfo.RedirectStandardOutput = true;
			startInfo.UseShellExecute = false;
			startInfo.FileName = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "TestDemo.exe");
			Console.WriteLine (startInfo.FileName);
			Process.Start (startInfo);
			Console.ReadLine ();
		}
	}
}
