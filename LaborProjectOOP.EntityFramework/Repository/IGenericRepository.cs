using Microsoft.EntityFrameworkCore;

namespace LaborProjectOOP.EntityFramework.Repository
{
	public interface IGenericRepository<TEntity> where TEntity : class
	{
		DbSet<TEntity> Table { get; }
		void Create(TEntity item);
		TEntity FindById(int id);
		IEnumerable<TEntity> Get();
		IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
		void Remove(TEntity item);
		void Update(TEntity item);
		void SaveChanges();
		Task CreateAsync(TEntity entity);
	}
	//public interface IGenericRepository<T> where T : class
	//{
	//	DbSet<T> Table { get; }
	//	Task Create(T entity);
	//	Task Delete(T entity);
	//	Task Update(T entity);
	//	Task<T> GetById(long id);
	//	Task<T> GetBy(Expression<Func<T, bool>> expression);
	//	IQueryable<T> GetAll();
	//	IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
	//}
}
