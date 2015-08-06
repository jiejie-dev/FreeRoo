using System;
using System.Linq;

namespace FreeRoo.Web
{
	public interface IOptionsService
	{
		Option GetSingleByID(string id);
		void Update(Option template);
		IQueryable<Option> Table{get;}
	}
}

