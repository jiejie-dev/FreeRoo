using System;
using System.Collections.Generic;

namespace FreeRoo.Developer
{
	public interface ICmdContainer
	{
		void AddCmdType (Type type);
		List<Type> GetAllCmdTypes ();
		string[] GetAllCmdNameList ();
	}
}

