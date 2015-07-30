using System;

namespace FreeRoo.Web
{
	public class DataConfig:IDataConfig
	{
		public string ConnectString{ get; set;}
		public string Prefix{ get; set;}
		public string DBName{ get; set;}
		public DataConfig (string connStr,string dbName,string prefix="")
		{
			this.ConnectString = connStr;
			this.DBName = dbName;
			this.Prefix = prefix;
		}
		public DataConfig()
		{
			this.ConnectString = "Server=127.0.0.1";
			this.DBName = "test";
		}
	}
}

