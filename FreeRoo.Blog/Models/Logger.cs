using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text;
using System.Runtime.InteropServices;

namespace FreeRoo.Blog
{
	/// <summary>
	/// 企业应用框架的日志类
	/// </summary>
	/// <remarks>此日志类提供高性能的日志记录实现。
	/// 当调用Write方法时不会造成线程阻塞,而是立即完成方法调用,因此调用线程不用等待日志写入文件之后才返回。</remarks>
	public class Logger: IDisposable,ILogger
	{
		//日志对象的缓存队列
		private static Queue<Log> logs;
		//日志文件保存的路径
		private static string path;
		//日志写入线程的控制标记
		private static bool state;
		//日志文件生命周期的时间标记
		private static DateTime TimeSign;
		//日志文件写入流对象
		private static StreamWriter writer;
		#region ILogger
		public void Error(string content)
		{
			Write (new Log(LogType.Error,content));
		}
		public void Warning(string content)
		{
			Write (new Log(LogType.Error,content));
		}
		public void Notice(string content)
		{
			Write (new Log(LogType.Notice,content));
		}
		public void Message(string content)
		{
			Write (new Log(LogType.Message,content));
		}
		public void Debug(string content)
		{
			Write (new Log(LogType.Debug,content));
		}
		#endregion
		/// <summary>
		/// 创建日志对象的新实例，根据指定的日志文件路径和指定的日志文件创建类型
		/// </summary>
		/// <param name="p">日志文件保存路径</param>
		/// <param name="t">日志文件创建方式的枚举</param>
		public Logger()
		{
			if (logs == null)
			{
				state = true;
				logs = new Queue<Log>();
				Thread thread = new Thread(work);
				thread.Start();
			}
		}

		//日志文件写入线程执行的方法
		private void work()
		{
			while (true)
			{
				//判断队列中是否存在待写入的日志
				if (logs.Count > 0)
				{
					Log Log = null;
					lock (logs)
					{
						Log = logs.Dequeue();
					}
					if (Log != null)
					{
						FileWrite(Log);
					}
				}
				else
				{
					//判断是否已经发出终止日志并关闭的消息
					if (state)
					{
						Thread.Sleep(1);
					}
					else
					{
						FileClose();
					}
				}
			}
		}

		//根据日志类型获取日志文件名，并同时创建文件到期的时间标记
		//通过判断文件的到期时间标记将决定是否创建新文件。
		private string GetFilename()
		{
			DateTime now = DateTime.Now;

			return now.ToString("yyyyMMddHHmmss");
		}

		//写入日志文本到文件的方法
		private void FileWrite(Log Log)
		{
			try
			{
				if (writer == null)
				{
					FileOpen();
				}
				else
				{
					//判断文件到期标志，如果当前文件到期则关闭当前文件创建新的日志文件
					if (DateTime.Now >= TimeSign)
					{
						FileClose();
						FileOpen();
					}
					writer.Write(Log.Time);
					writer.Write('\t');
					writer.Write(Log.LogType);
					writer.Write('\t');
					writer.WriteLine(Log.Content);
					writer.Flush();
				}
			}
			catch (Exception e)
			{
				Console.Out.Write(e);
			}
		}

		//打开文件准备写入
		private void FileOpen()
		{
			writer = new StreamWriter(path + GetFilename(), true, Encoding.UTF8);
		}

		//关闭打开的日志文件
		private void FileClose()
		{
			if (writer != null)
			{
				writer.Flush();
				writer.Close();
				writer.Dispose();
				writer = null;
			}
		}

		/// <summary>
		/// 写入新日志，根据指定的日志对象Log
		/// </summary>
		/// <param name="Log">日志内容对象</param>
		public void Write(Log Log)
		{
			if (Log != null)
			{
				lock (logs)
				{
					logs.Enqueue(Log);
				}
			}
		}

		#region IDisposable 成员

		/// <summary>
		/// 销毁日志对象
		/// </summary>
		public void Dispose()
		{
			state = false;
		}

		#endregion
	}
}