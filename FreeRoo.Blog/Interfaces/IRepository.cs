using System;
using System.Linq;
using System.Linq.Expressions;

namespace FreeRoo.Blog
{
	public interface IRepository<T>
	{
		T Get(Expression<Func<T, bool>> predicate);
		void Insert(T entity);
		void Update(T entity);
		void Update(Action<T> updateAction);
		void Delete(T entity);
		void Delete (Action<T> deleteAction);
		IQueryable<T> Table { get; }
		int Count(Expression<Func<T,bool>> predicate);
	}
}