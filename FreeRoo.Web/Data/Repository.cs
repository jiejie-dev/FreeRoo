using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using FreeRoo;
using System.Security.Cryptography.X509Certificates;

namespace FreeRoo.Web
{
	public class Repository<T>:IRepository<T> where T : class
	{
		private MongodbHelper<T> _dbHelper;

		public Repository ()
		{
			this._dbHelper = new MongodbHelper<T> (new MongoDataConfig ());
		}

		public T Get (Expression<Func<T, bool>> predicate)
		{
			return Table.Where (predicate).FirstOrDefault ();
		}

		public void Insert (T entity)
		{
			this._dbHelper.Insert (entity);
		}

		public void Update (T entity, Expression<Func<T,bool>> exp)
		{
			this._dbHelper.Update (entity, exp);
		}

		public void Delete (Expression<Func<T,bool>> exp)
		{
			this._dbHelper.Delete (exp);
		}

		public int Count (Expression<Func<T,bool>> predicate)
		{
			return this._dbHelper.Count (predicate);
		}

		public IQueryable<T> Table { 
			get {
				return this._dbHelper.GetTable ();
			}
		}

		public int Count ()
		{
			return this.Table.Count ();
		}
	}
}
