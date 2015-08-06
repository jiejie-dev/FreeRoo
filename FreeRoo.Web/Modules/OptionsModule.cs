using System;
using System.Linq;

namespace FreeRoo.Web
{
	public class TemplateModule:BaseModule
	{
		public TemplateModule (ITemplateService service)
			:base("/options")
		{
			Get ["/options"] = _ => {
				string template=MarkdownTemplate.index;
				var templates=service.Table.ToList ();
				string current="";
				foreach(var item in templates){
					current+=string.Format ("##[{0}](/template/{1})",item.Slug,item.Slug);
				}
				return template.Replace ("{content}",current);
			};
			Get ["/template/{slug}"] = _ => {
				string slug = _.slug;
				var current = service.Table.FirstOrDefault(item=>item.Slug==slug);
				var template = service.Table.FirstOrDefault (item=>item.Slug=="template_editor");
				string result = template.Content.Replace ("{id}",current.ID);
				result = result.Replace ("{slug}",current.Slug);
				result = result.Replace ("{content}",current.Content);
				return result;
			};
		}
	}
}

