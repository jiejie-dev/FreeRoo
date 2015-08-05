using System;
using System.Reflection;
using System.Linq;

namespace FreeRoo.Developer
{
	public class DeveoperStarter:IDeveloperStarter
	{
		public DeveoperStarter ()
		{
		}
		public void Start()
		{
			CommandLine.Hyphen ();
			Console.WriteLine ("       Init Developer Tools     ");
			CommandLine.Hyphen ();
			DevelopLooper.Current = new DevelopLooper ();
			DevelopLooper.Current.StartLoop ();
		}
	}
}

