using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LaborProjectOOP.EntityFramework.Repository
{
	public interface IGenericRepository<T> where T : class
	{
		DbSet<T> Table { get; }
		Task Create(T entity);
		Task Delete(T entity);
		Task Update(T entity);
		Task<T> GetById(long Id);
		Task<T> GetBy(Expression<Func<T, bool>> expression);
		Task SaveChanges();
		IQueryable<T> GetAll();
		IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
	}
}
