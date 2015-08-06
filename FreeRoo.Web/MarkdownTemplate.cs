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
			"---" +
			"{copyright}\n" +
			"</xmp>\n\n" +
			"<script src=\"{strapdown}\"></script>\n" +
			"</html>";
		public static string index_item_template="## [{article.title}]({article.href})\n";
		public static string article_template="## [{article.title}]({article.href})\n{article.content}";
		public static string editor_template="<!DOCTYPE html>\n<html lang=\"zh\">\n\n<head>\n    <meta charset=\"utf-8\" />\n    <title>{title}</title>\n    <link rel=\"stylesheet\" href=\"/public/editor.md/css/editormd.css\" />\n    <!-- <link ref=\"stylesheet\" href=\"http://apps.bdimg.com/libs/bootstrap/3.3.4/css/bootstrap.min.css\"> -->\n    <!--<script src=\"http://apps.bdimg.com/libs/jquery/2.1.4/jquery.min.js\"></script>-->\n    <!-- // <script src=\"./public/editor.md/js/jquery.min.js\"></script> -->\n    <!-- // <script src=\"http://apps.bdimg.com/libs/bootstrap/3.3.4/js/bootstrap.min.js\"></script> -->\n    <link rel=\"stylesheet\" href=\"http://apps.bdimg.com/libs/bootstrap/3.3.0/css/bootstrap.min.css\">\n    <script src=\"http://apps.bdimg.com/libs/jquery/2.1.1/jquery.min.js\"></script>\n    <script src=\"http://apps.bdimg.com/libs/bootstrap/3.3.0/js/bootstrap.min.js\"></script>\n</head>\n\n<body>\n    <nav class=\"navbar navbar-default\" role=\"navigation\">\n        <div class=\"container\">\n            <div class=\"navbar-header\">\n                <a class=\"navbar-brand\" href=\"#\">lujiejie</a>\n            </div>\n            <div>\n                <button id=\"save\" type=\"button\" class=\"btn btn-default navbar-btn pull-right\">\n                    Save\n                </button>\n            </div>\n        </div>\n    </nav>\n    <div class=\"container\">\n        <form role=\"form\" id=\"editor-form\">\n            <div class=\"form-group\">\n                <label for=\"title\">Title</label>\n                <input type=\"text\" name=\"Title\" id=\"title\" class=\"form-control\" value=\"{title}\" />\n            </div>\n            <div class=\"form-group\">\n                <label for=\"slug\">Slug</label>\n                <input type=\"text\" name=\"Slug\" id=\"slug\" class=\"form-control\" value=\"{slug}\" />\n            </div>\n            <div class=\"form-group\">\n                <label for=\"tag\">Tags</label>\n                <input type=\"text\" name=\"Tag\" id=\"tag\" class=\"form-control\" value=\"{tags}\" />\n            </div>\n            <div id=\"article-editormd\">\n                <textarea style=\"display:none;\" name=\"Content\">{content}</textarea>\n            </div>\n        </form>\n    </div>\n    <script src=\"/public/editor.md/editormd.min.js\"></script>\n    <script type=\"text/javascript\">\n    var post_url = \"{post_url}\";\n    $(\"#save\").click(function() {\n        var post = $(\"#editor-form\").serialize();\n        $.post(post_url, post, function(data) {\n            alert(\"Saved !\");\n        });\n    });\n    var editor;\n\n    $(function() {\n        editor = editormd(\"article-editormd\", {\n            // width: \"90%\",\n            height: 640,\n            syncScrolling: \"single\",\n            path: \"/public/editor.md/lib/\"\n        });\n\n        /*\n        // or\n        testEditor = editormd({\n            id      : \"test-editormd\",\n            width   : \"90%\",\n            height  : 640,\n            path    : \"../lib/\"\n        });\n        */\n    });\n    </script>\n</body>\n\n</html>\n";
	}
}

