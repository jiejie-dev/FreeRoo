using System;
using System.Linq;

namespace FreeRoo.Web
{
	public class OptionsService:IOptionsService
	{
		private IRepository<Option> repository;
		public OptionsService (IRepository<Option> repository)
		{
			this.repository = repository;
		}
		public void Update(Option opt)
		{
			this.repository.Update (opt, x => x.ID == opt.ID);
		}
		public Option GetSingleByID(string id)
		{
			return this.repository.Get (opt => opt.ID == id);
		}
		public IQueryable<Option> Table{
			get {
				return this.repository.Table;
			}
		}

	}
}

