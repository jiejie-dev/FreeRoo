using System;
using System.Collections.Generic;

namespace FreeRoo.Web
{
	/// <summary>
	/// 首页数据模型
	/// </summary>
	public class IndexRenderData:PageData
	{
		public List<Article> Articles{ get; set; }
		public IndexRenderData ()
		{
			this.Articles = new List<Article> ();
		}
	}
}

