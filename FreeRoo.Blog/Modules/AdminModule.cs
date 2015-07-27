using System;
using Nancy;

namespace FreeRoo.Blog
{
	public class AdminModule:NancyModule
	{
		public AdminModule (IRepository<IUser> service)
		{
			Post ["/article"] = _ => "post";
		}
	}
}

