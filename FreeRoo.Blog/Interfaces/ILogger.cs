using System;

namespace FreeRoo.Blog
{
	public interface ILogger
	{
		void Error(string content);
		void Warning(string content);
		void Notice(string content);
		void Message (string content);
		void Debug(string content);
	}
}

