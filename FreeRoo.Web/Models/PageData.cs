using System;

namespace FreeRoo.Web
{
	public class PageData
	{
		public string Title{ get; set; }
		public string Description{ get; set; }
		public string KeyWords{ get; set; }
		public PageData ()
		{
			this.Title = "Welcome to FreeRoo ! Powered by FreeRoo !";
			this.Description =  "Welcome to FreeRoo ! Powered by FreeRoo !";
			this.KeyWords = "FreeRoo";
		}
	}
}

