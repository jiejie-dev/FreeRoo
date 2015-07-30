using System;
using System.Threading;

namespace FreeRoo.Web
{
	public enum MessageCode
	{
		SUCCESS = 4001,
		ERROR = 4002,
		USER_DENY = 4003,
		API_NOT_EXIST = 4004,
		FILE_NOT_EXIST = 4005,
		USER_PWD_UNCORRECT = 4006,
		FAILUER = 4007
	}

	public class Message
	{
		public static Message Success = new Message (true, "success", DateTime.Now, null, MessageCode.SUCCESS);
		public static Message Failure = new Message (false, "failure", DateTime.Now, null, MessageCode.FAILUER);

		public bool Flag{ get; set; }

		public string Msg{ get; set; }

		public DateTime Time { get; set; }

		public object Data{ get; set; }

		public MessageCode Code{ get; set; }

		public  Message (bool flag, string msg, DateTime time, object data, MessageCode code)
		{
			this.Flag = flag;
			this.Msg = msg;
			this.Data = data;
			this.Time = time;
			this.Code = code;
		}
	}
}

