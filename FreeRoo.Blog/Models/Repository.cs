using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FreeRoo.Blog
{
	public class Repository<T>:IRepository<T> where T : class
	{

		public Repository ()
		{
			
		}

		public T Get(Expression<Func<T, bool>> predicate)
		{
			return Table.Where (predicate).FirstOrDefault ();
		}
		public void Insert(T entity)
		{
			using (MongoDatabase db = (new MongoClient (_connectionString)).GetDatabase (_prefix + typeof(T).Name)) {
				db.GetCollection <T> (_prefix + typeof(T).Name).Insert (entity);
			}
		}
		public void Update(T entity)
		{
			MongodbHelper helper = new MongodbHelper<T> ();
		}
		public void Update(Action<T> updateAction)
		{

		}
		public void Delete(T entity)
		{
			
		}
		public void Delete (Action<T> deleteAction)
		{

		}
		public int Count(Expression<Func<T,bool>> predicate)
		{
			return 0;
		}
		public IQueryable<T> Table { get; }
	}
}

