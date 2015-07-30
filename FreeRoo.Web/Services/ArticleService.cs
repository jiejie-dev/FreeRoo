using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FreeRoo.Web
{
	public class ArticleService:IArticleService
	{
		private IRepository<Article> repository;

		public ArticleService (IRepository<Article> repository)
		{
			this.repository = repository;
		}

		#region IArticleService implementation

		public Article GetSingleByID (string id)
		{
			return this.repository.Table.FirstOrDefault (a => a.ID == id);
		}

		public void AddArticle (Article article)
		{
			this.repository.Insert (article);
		}

		public void DeleteArticle (Article article)
		{
			this.repository.Delete (a => a.ID == article.ID);
		}

		public void UpdateArticle (Article article)
		{
			this.repository.Update (article, a => a.ID == article.ID);
		}

		public List <Article> List (Expression<Func<Article, bool>> exp)
		{
			return this.repository.Table.Where (exp).ToList ();
		}

		public int Count(Expression<Func<Article,bool>> exp)
		{
			return this.repository.Count (exp);
		}

		public int Count()
		{
			return this.repository.Count ();
		}

		public IQueryable<Article> Table{ 
			get{ return this.repository.Table; } 
		}
		#endregion

	}
}

