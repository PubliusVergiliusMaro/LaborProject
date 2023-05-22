using Microsoft.EntityFrameworkCore;

namespace LaborProjectOOP.EntityFramework.Repository
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
	{
		DbContext _context;
		public DbSet<TEntity> Table
		{
			get
			{
				return _context.Set<TEntity>();
			}
		}
		public GenericRepository(DbContext context)
		{
			_context = context;
		}

		public IEnumerable<TEntity> Get()
		{
			return Table.AsNoTracking().ToList();
		}

		public IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
		{
			return Table.AsNoTracking().Where(predicate).ToList();
		}
		public TEntity FindById(int id)
		{
			return Table.Find(id);
		}

		public void Create(TEntity item)
		{
			Table.Add(item);
			_context.SaveChanges();
		}
		public void Update(TEntity item)
		{
			_context.Entry(item).State = EntityState.Modified;
			_context.SaveChanges();
		}
		public void Remove(TEntity item)
		{
			Table.Remove(item);
			_context.SaveChanges();
		}

		public async void SaveChanges()
		{
			await _context.SaveChangesAsync();
		}

		public async Task CreateAsync(TEntity entity)
		{
			await _context.Set<TEntity>().AddAsync(entity);
			await _context.SaveChangesAsync();
		}
	}
	//public class GenericRepository<T> : IGenericRepository<T> where T : class
	//{
	//	private readonly ApplicationContext _context;

	//	public GenericRepository(ApplicationContext context)
	//	{
	//		_context = context;
	//	}

	//	public DbSet<T> Table => _context.Set<T>();

	//	public async Task Create(T entity)
	//	{
	//		await Table.AddAsync(entity);
	//		await _context.SaveChangesAsync();
	//	}

	//	public async Task Delete(T entity)
	//	{
	//		Table.Remove(entity);
	//		await _context.SaveChangesAsync();
	//	}

	//	public IQueryable<T> GetAll()
	//	{
	//		return Table.AsNoTracking();
	//	}

	//	public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
	//	{
	//		return Table.Where(expression);
	//	}

	//	public async Task<T> GetBy(Expression<Func<T, bool>> expression)
	//	{
	//		return await Table.FirstOrDefaultAsync(expression);
	//	}

	//	public async Task<T> GetById(long Id)
	//	{
	//		return await Table.FindAsync(Id);
	//	}

	//	public async Task Update(T entity)
	//	{
	//		Table.Update(entity);
	//		await _context.SaveChangesAsync();
	//	}
	//}
}
