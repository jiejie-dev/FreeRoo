using System;
using System.Net;

namespace FreeRoo.Web
{
	public class WebToolService:IWebToolService
	{
		public WebToolService ()
		{
			
		}
		public StanSoft.Article HtmlContent2Article(string html)
		{
			return StanSoft.Html2Article.GetArticle (html);
		}
		public StanSoft.Article HtmlUrl2Article(string url)
		{
			WebClient client = new WebClient ();
			var html = client.DownloadString (url);
			return HtmlContent2Article (html);
		}
	}
}

