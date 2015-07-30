using System;
using System.Data.Linq;
using Nancy;
using System.Linq;
using Nancy.ModelBinding;

namespace FreeRoo.Web
{
	public class ArticleModule:BaseModule
	{
		public ArticleModule (IArticleService service)
			:base("/article")
		{
			Get ["/tag"] = _ => {
				return "Tags";
			};
			Get ["/{slug}"] = _ => {
				return "";
			};
			Get ["/tag/{tag}"] = _ => {
				string tag = _.tag;
				return Response.AsJson (service.List(a=>a.Tag.IndexOf (tag)>-1));
			};
			Get ["/"] = _ => {
				return Response.AsJson (service.Table.ToList ());
			};
			Post ["/"] = _ => {
				var article=this.Bind<Article>();
				service.AddArticle (article);
				return Response.AsJson (Message.Success);
			};
			Put ["/"] = _ => {
				var data =this.Bind <Article>();
				service.UpdateArticle (data);
				return Response.AsJson (Message.Success);
			};
			Delete ["/id"] = _ => {
				return "";
			};
		}
	}
}

