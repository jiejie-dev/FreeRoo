using System;
using Nancy;
using Nancy.Bootstrapper;
using System.Threading;
using Nancy.TinyIoc;

namespace FreeRoo.Blog
{
	public class BlogBootstrapper:DefaultNancyBootstrapper
	{
		public BlogBootstrapper ()
		{
			
		}

		protected override void ApplicationStartup (TinyIoCContainer container, IPipelines pipelines)
		{
			container.Register <IUser,User>();
			container.Register <IArticle,Article> ();
			container.Register <IArticleService> ();
			container.Register <IMarkdown,Markdown> ();
			container.Register (typeof(IRepository<>), typeof(Repository<>)).AsMultiInstance ();
			base.ApplicationStartup (container, pipelines);
		}
	}
}

