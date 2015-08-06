using System;

namespace FreeRoo.Web
{
	public class InstallModule:BaseModule
	{
		public InstallModule (IOptionsService service)
		{
			Get ["/install"] = _ => {
				string install_template = "<html>" +
					"<";
				return install_template;
			};
			Get ["/install/do"] = _ => {
				return "install_do";
			};
		}
	}
}

