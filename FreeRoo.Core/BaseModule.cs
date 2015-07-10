using System;
using Nancy;
using Owin;
using System.Reflection;

namespace FreeRoo.Core
{
	public class BaseModule:NancyModule
	{
		public BaseModule ()
		{
			Get [""] = _ => View ["Index"];
		}
	}
}

