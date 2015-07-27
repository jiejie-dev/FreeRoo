using System;
using System.Collections.Generic;

namespace FreeRoo.Blog
{
	public class IndexRenderData:IIndexRenderData
	{
		public List<Article> Articles{ get; set; }
		public IndexRenderData ()
		{
		}
	}
}

