using System;

namespace FreeRoo.Developer
{
	public interface ICmdContext
	{
		IDevelopLooper GetDevelopLooper ();
		ICmdContainer GetCmdContainer();
	}
}

