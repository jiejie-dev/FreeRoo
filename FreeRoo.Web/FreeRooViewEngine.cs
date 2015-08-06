using System;
using Nancy.ViewEngines;
using System.Collections.Generic;
using Nancy;
using System.Runtime.CompilerServices;

namespace FreeRoo.Web
{
	public class FreeRooViewEngine:IViewEngine
	{
		public FreeRooViewEngine ()
		{
		}

		public IEnumerable<string> Extensions {
			get{
				return null;
			}
		}

		public void Initialize (ViewEngineStartupContext viewEngineStartupContext)
		{

		}

		public Response RenderView (ViewLocationResult viewLocationResult,dynamic model, IRenderContext renderContext)
		{
			return null;
		}
	}
}

