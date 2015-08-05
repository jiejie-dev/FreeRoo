using System;
using System.Net.Mime;

namespace FreeRoo.Web
{
	public class MarkdownTemplate
	{
		public static string site_name="lujiejie";
		public static string copy_right="Copyright [lujiejie.com](http://lujiejie.com) " +
			"Powerd by [Nancy](http://nancyfx.org)," +
			"[Strapdownjs](http://strapdownjs.com)";
		public static string strapdown_js="/public/strapdown/strapdown.js";
		public static string index_template="<!DOCTYPE html>\n" +
			"<html>\n<title>{site.name}</title>\n\n" +
			"<xmp {theme} style=\"display:none;\">\n\n" +
			"{content}\n" +
			"{copyright}\n" +
			"</xmp>\n\n" +
			"<script src=\"{strapdown}\"></script>\n" +
			"</html>";
		public static string index_item_template="## [{article.title}]({article.href})\n";
		public static string article_template="## [{article.title}]({article.href})\n{article.content}";
	}
}

