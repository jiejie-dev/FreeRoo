using System;

namespace FreeRoo.Web
{
	public class MdModule:BaseModule
	{
		public MdModule ()
			:base("/md")
		{
			Get ["/{name}"] = _ => {

			};
		}
	}
}

