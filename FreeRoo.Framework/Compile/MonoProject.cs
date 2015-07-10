using System;
using System.ComponentModel.Design;

namespace FreeRoo.Framework
{
	
	public class MonoProject:IProject
	{
		public string[] CSFiles{ get; set; }

		public string AssemblyName{ get; set; }

		public ProjectOutPutType OutPutType{ get; set; }

		public string[] References{ get; set; }

		public MonoProject ()
		{
		}
	}
}

