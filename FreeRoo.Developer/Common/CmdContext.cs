using System;

namespace FreeRoo.Developer
{
	public class CmdContext:ICmdContext
	{
		public CmdContext ()
		{
		}
		public IDevelopLooper GetDevelopLooper ()
		{
			return DevelopLooper.Current;
		}
		public ICmdContainer GetCmdContainer()
		{
			return new CmdContainer ();
		}
	}
}

