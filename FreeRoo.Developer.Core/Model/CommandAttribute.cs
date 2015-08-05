using System;

namespace FreeRoo.Developer
{
	
	public class CommandAttribute:Attribute
	{
		public string Description{ get; set;}
		public string Format{ get; set;}
		public string Args{ get;set;}
		public CommandAttribute (string description,string format,string args)
		{
			this.Description = description;
			this.Format = format;
			this.Args = args;
		}
	}
}

