using System;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace FreeRoo.Web
{
	public interface IArticleService:IDependency
	{
		Article GetSingleByID (string id);
		void AddArticle(Article article);
		void DeleteArticle(Article article);
		void UpdateArticle(Article article);
		List <Article>  List(Expression<Func<Article,bool>> exp);
		IQueryable<Article> Table{ get; }
		int Count(Expression<Func<Article,bool>> exp);
		int Count();
	}
}

