using System;
using System.Threading;

namespace FreeRoo.Web
{
	public class Log:IDependency
	{
		public LogType LogType{ get; set; }
		public string Content{ get; set; }
		public DateTime Time{ get; set; }
		public Log (LogType type,string content)
		{
			this.LogType = type;
			this.Content = content;
			this.Time = DateTime.Now;
		}
		public override string ToString ()
		{
			return string.Format ("[Log: LogType={0}, Content={1}, Time={2}]", LogType, Content, Time);
		}
	}
}

