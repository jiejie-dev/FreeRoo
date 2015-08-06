using System;
using System.Net.Mime;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace FreeRoo.Web
{
	public class TemplatesCache
	{
		public static TemplatesCache Current{ get; }

		public Dictionary<string,string> Items{get;private set;}
		public TemplatesCache(){
			this.ReFresh ();
		}
		public void ReFresh(){
			Items = new Dictionary<string, string> ();
			DirectoryInfo dir = new DirectoryInfo (Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "public", "template"));
			var files = dir.GetFiles ();
			foreach (var item in files) {
				Items.Add (item.Name,File.ReadAllText (item.FullName,Encoding.Default));
			}
		}
	}
}

