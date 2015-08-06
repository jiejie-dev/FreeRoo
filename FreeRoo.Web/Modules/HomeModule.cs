using System;
using Nancy;
using System.Linq;
using System.Text;

namespace FreeRoo.Web
{
	public class HomeModule:BaseModule
	{
		public HomeModule (IArticleService service)
			:base()
		{
			Get ["/"] = _ => {
				
				StringBuilder sb=new StringBuilder();
				var articles=service.Table.Take (15);
				foreach(var item in articles){
					string tmp=TemplatesCache.Current.Items["index_item_template"].Replace ("{article.title}",item.Title);
					tmp=tmp.Replace ("{article.href}","/article/"+item.Slug);
					tmp=tmp.Replace ("{article.content}",item.Content);
					sb.AppendLine(tmp);
					sb.AppendLine ("---");
				}
				string index_template=TemplatesCache.Current.Items["index_template"];
				index_template=index_template.Replace ("{site.name}",TemplatesCache.Current.Items["site_name"]);
				index_template=index_template.Replace ("{theme}","");
				index_template= index_template.Replace ("{content}",sb.ToString ());
				index_template=index_template.Replace ("{strapdown}",TemplatesCache.Current.Items["strapdown_js"]);
				index_template=index_template.Replace ("{copyright}",TemplatesCache.Current.Items["copy_right"]);
				return index_template;
			};
		}
	}
}

