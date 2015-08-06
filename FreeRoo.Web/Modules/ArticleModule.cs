using System;
using System.Data.Linq;
using Nancy;
using System.Linq;
using Nancy.ModelBinding;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;

namespace FreeRoo.Web
{
	public class ArticleModule:BaseModule
	{
		public ArticleModule (IArticleService service)
			: base ("/article")
		{
			Get ["/tag"] = _ => {
				return "Tags";
			};
			Get ["/{slug}"] = _ => {
				string slug = _.slug;
				var article = service.Table.FirstOrDefault (ssss => ssss.Slug == slug);
				if (article != null) {
					string article_html = TemplatesCache.Current.Items["article_template"].Replace ("{article.content}", article.Content);
					article_html = article_html.Replace ("{article.title}", article.Title);
					article_html=article_html.Replace ("{article.href}","/article/"+article.Slug);
					article_html=TemplatesCache.Current.Items["index_template"].Replace ("{content}",article_html);
					article_html=article_html.Replace ("{strapdown}",TemplatesCache.Current.Items["strapdown_js"]);
					article_html=article_html.Replace ("{site.name}",TemplatesCache.Current.Items["site_name"]);
					article_html=article_html.Replace ("{copyright}",TemplatesCache.Current.Items["copy_right"]);
					return article_html;
				} else {
					return "Article Not Found !";
				}
			};
			Get ["/tag/{tag}"] = _ => {
				string tag = _.tag;
				return Response.AsJson (service.List (a => a.Tag.IndexOf (tag) > -1));
			};
			Get ["/"] = _ => {
				return Response.AsJson (service.Table.ToList ());
			};
			Post ["/"] = _ => {
				var article = this.Bind<Article> ();
				article.CreateTime=DateTime.Now;
				article.ID=DateTime.Now.ToString ("yyyyMMdd")+service.Table.Count ();
				service.AddArticle (article);
				return Response.AsJson (Message.Success);
			};
			Post ["/update"] = _ => {
				var article = this.Bind <Article>();
				service.UpdateArticle (article);
				return Response.AsJson (Message.Success);
			};
			Put ["/"] = _ => {
				var data = this.Bind <Article> ();
				service.UpdateArticle (data);
				return Response.AsJson (Message.Success);
			};
			Delete ["/id/{id}"] = _ => {
				string id=_.id;
				var article=service.GetSingleByID (id);
				service.DeleteArticle (article);
				return Response.AsJson (Message.Success);
			};
			Get ["/editor/{id}"] = _ => {
				string id=_.id;
				var article = service.GetSingleByID (id);
				string editor_template = TemplatesCache.Current.Items["editor_template"];
				editor_template=editor_template.Replace ("{post_url}","/article/update");
				editor_template=editor_template.Replace ("{title}",article.Title);
				editor_template=editor_template.Replace ("{slug}",article.Slug);
				editor_template=editor_template.Replace ("{tags}",article.Tag);
				editor_template=editor_template.Replace ("{content}",article.Content);
				return editor_template;
			};
			Get ["/editor"] = _ => {
				string editor_template = TemplatesCache.Current.Items["editor_template"];
				editor_template=editor_template.Replace ("{post_url}","/article/");
				editor_template=editor_template.Replace ("{title}","");
				editor_template=editor_template.Replace ("{slug}","");
				editor_template=editor_template.Replace ("{tags}","");
				editor_template=editor_template.Replace ("{content}","");
				return editor_template;
			};
		}
	}
}

