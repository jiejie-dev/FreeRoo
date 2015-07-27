using System;
using Nancy;
using Owin;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace FreeRoo.Core.Modules
{
	public class BaseModule:NancyModule
	{
		public BaseModule ()
		{
			Get ["/"] = _ => "this is freeroo index !";
			Get ["/ha"] = _=> {
				
			};
			this.After+=ctx=>{
				ctx.Response;
				return "haha";
			};
		}
	}
}