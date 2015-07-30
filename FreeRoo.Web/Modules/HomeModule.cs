using System;
using Nancy;

namespace FreeRoo.Web
{
	public class HomeModule:BaseModule
	{
		public HomeModule (IArticleService service)
			:base()
		{
			Get ["/"] = _ => {
				ViewBag.Brand = "Lujiejie";
				var data = new IndexRenderData ();
				data.Articles = service.List (null);
				return View ["index",data];
			};
		}
	}
}

