using System;

namespace FreeRoo.Web
{
	public class Article
	{
		public Article ()
		{
		}
		public string ID{get;set;}
		public DateTime CreateTime{ get; set; }
		public DateTime UpdateTime{ get; set; }
		public string Tag{get;set;}
		public string Content{get;set;}
		public string Title{get;set;}
		public User Author{ get; set; }
		public string Slug{ get; set; }
	}
}

