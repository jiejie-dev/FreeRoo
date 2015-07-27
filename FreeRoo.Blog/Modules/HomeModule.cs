using System;
using Nancy;

namespace FreeRoo.Blog
{
	public class HomeModule:NancyModule
	{
		public HomeModule ()
		{
			Get ["/"] = _ => "FreeRoo.Blog";
		}
	}
}

