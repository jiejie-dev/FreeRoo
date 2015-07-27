using System;
using System.Reflection;
using System.IO;
using FreeRoo.Framework;

namespace FreeRoo.Core
{
	public class FreeRooAppStarter
	{
		public FreeRooAppStarter ()
		{
			FreeRooRuntime.Resource = new Resource ();
		}
	}
}

