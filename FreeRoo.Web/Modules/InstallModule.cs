using System;

namespace FreeRoo.Web
{
	public class InstallModule:BaseModule
	{
		public InstallModule (ITemplateService service)
		{
			Get ["/install"] = _ => {
				string install_template = "<html>" +
					"<";
				return install_template;
			};
			Get ["/install/do"] = _ => {

			};
		}
	}
}

