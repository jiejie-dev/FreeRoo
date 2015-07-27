using System;

namespace FreeRoo.Blog
{
	public class User:IUser
	{
		public string UserID{get;set;}
		public string UserName{get;set;}
		public string PassWord{ get; set; }

		public User ()
		{
		}
		public void Get()
		{

		}
	}
}

