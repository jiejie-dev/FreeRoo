using System;

namespace FreeRoo.Web
{
	public class WebToolModule:BaseModule
	{
		public WebToolModule(IWebToolService service)
			:base("/webtool")
		{
			Post ["/html2article"] = _ => {
				return "html2aricle";
			};
		}
	}
}

