using System;
using Nancy;

namespace FreeRoo.Web
{
	public class BaseModule:NancyModule
	{
		public BaseModule()
		{

		}

		public BaseModule (string path)
			:base(path)
		{
		}
	}
}

