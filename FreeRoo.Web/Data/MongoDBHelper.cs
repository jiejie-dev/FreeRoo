using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using MongoDB;
using MongoDB.Attributes;
using MongoDB.Configuration;
using MongoDB.Linq;
using FreeRoo.Web;

namespace FreeRoo
{
	public class MongodbHelper<T>:IDisposable where T : class
	{
		#region IDisposable implementation

		public void Dispose ()
		{
			this.mongo.Disconnect ();
		}

		#endregion

		string connectionString = string.Empty;
		string databaseName = string.Empty;
		string collectionName = string.Empty;
		private Mongo mongo;

		#region 初始化操作

		/// <summary>
		/// 初始化操作
		/// </summary>
		public MongodbHelper (MongoDataConfig config)
		{
			connectionString = config.ConnectString;
			databaseName = config.DBName;
			collectionName = typeof(T).Name;
			this.mongo = new Mongo (configuration);
			this.mongo.Connect ();
		}

		#endregion

		#region 实现linq查询的映射配置

		/// <summary>
		/// 实现linq查询的映射配置
		/// </summary>
		public MongoConfiguration configuration {
			get {
				var config = new MongoConfigurationBuilder ();
				config.Mapping (mapping => {
					mapping.DefaultProfile (profile => {
						profile.SubClassesAre (t => t.IsSubclassOf (typeof(T)));
					});
					mapping.Map<T> ();
					mapping.Map<T> ();
				});
				config.ConnectionString (connectionString);
				return config.BuildConfiguration ();
			}
		}

		#endregion

		#region 插入操作

		/// <summary>
		/// 插入操作
		/// </summary>
		/// <param name="person"></param>
		/// <returns></returns>
		public void Insert (T t)
		{
			try {
				var db = mongo.GetDatabase (databaseName);
				var collection = db.GetCollection<T> (collectionName);
				collection.Insert (t, true);
				mongo.Disconnect ();
			} catch (Exception) {
				throw;
			}
		}

		#endregion

		#region 更新操作

		/// <summary>
		/// 更新操作
		/// </summary>
		/// <param name="person"></param>
		/// <returns></returns>
		public void Update (T t, Expression<Func<T, bool>> func)
		{
			try {
				var db = mongo.GetDatabase (databaseName);
				var collection = db.GetCollection<T> (collectionName);
				collection.Update<T> (t, func, true);
			} catch (Exception) {
				throw;
			}
		}

		#endregion

		#region 获取集合

		/// <summary>
		///获取集合
		/// </summary>
		/// <param name="person"></param>
		/// <returns></returns>
		public List<T> List (int pageIndex, int pageSize, Expression<Func<T, bool>> func, out int pageCount)
		{
			pageCount = 0;
			try {
				var db = mongo.GetDatabase (databaseName);
				var collection = db.GetCollection<T> (collectionName);
				pageCount = Convert.ToInt32 (collection.Count ());
				var personList = collection.Linq ().Where (func).Skip (pageSize * (pageIndex - 1))
						.Take (pageSize).Select (i => i).ToList ();
				return personList;
			} catch (Exception) {
				throw;
			}
		}

		public IQueryable<T> GetTable ()
		{
			try {
				mongo.Connect ();
				var db = mongo.GetDatabase (databaseName);
				var collection = db.GetCollection<T> (collectionName);
				var table = collection.Linq ();
				return table;
			} catch (Exception) {
				throw;
			}

		}

		public List <T> GetTableList ()
		{
			try {
				var db = mongo.GetDatabase (databaseName);
				var collection = db.GetCollection<T> (collectionName);
				var table = collection.Linq ().ToList ();
				return table;
			} catch (Exception) {
				throw;
			}
		}

		#endregion

		#region 读取单条记录

		/// <summary>
		///读取单条记录
		/// </summary>
		/// <param name="person"></param>
		/// <returns></returns>
		public T Single (Expression<Func<T, bool>> func)
		{
			try {
				var db = mongo.GetDatabase (databaseName);
				var collection = db.GetCollection<T> (collectionName);
				var single = collection.Linq ().FirstOrDefault (func);
				return single;
			} catch (Exception) {
				throw;
			}
		}

		#endregion

		#region 删除操作

		/// <summary>
		/// 删除操作
		/// </summary>
		/// <param name="person"></param>
		/// <returns></returns>
		public void Delete (Expression<Func<T, bool>> func)
		{
			try {
				var db = mongo.GetDatabase (databaseName);
				var collection = db.GetCollection<T> (collectionName);
				//这个地方要注意，一定要加上T参数，否则会当作object类型处理
				//导致删除失败
				collection.Remove<T> (func);
			} catch (Exception) {
				throw;
			}
		}

		#endregion

		#region 数据数量

		public int Count (Expression<Func<T,bool>> predicate)
		{
			try {
				
				if (predicate == null)
					return this.GetTable ().Count ();
				else
					return this.GetTable ().Where (predicate).Count ();
			} catch {
				throw;
			}
		}

		#endregion
	}
	#region 数据实体
	/// <summary>
	/// 数据实体
	/// </summary>
	public class Person
	{
		[MongoAlias ("_id")]
		public string ID { get; set; }

		public string Name { get; set; }

		public int Age { get; set; }

		public DateTime CreateTime { get; set; }
	}
	#endregion
}