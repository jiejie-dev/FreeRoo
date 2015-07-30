using System;
using System.Linq;
using System.Linq.Expressions;

namespace FreeRoo.Web
{
	public interface IRepository<T>
	{
		T Get(Expression<Func<T, bool>> predicate);
		void Insert(T entity);
		void Update (T entity, Expression<Func<T,bool>> exp);
		void Delete (Expression<Func<T,bool>> exp);
		IQueryable<T> Table { get; }
		int Count(Expression<Func<T,bool>> predicate);
		int Count();
	}
}