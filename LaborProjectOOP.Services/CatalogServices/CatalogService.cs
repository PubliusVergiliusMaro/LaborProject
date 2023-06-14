using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace LaborProjectOOP.Services.CatalogServices
{
	public class CatalogService : ICatalogService
	{
		private readonly IGenericRepository<CatalogEntity> _catalogRepository;

		public CatalogService(IGenericRepository<CatalogEntity> catalogRepository)
		{
			_catalogRepository = catalogRepository;
		}

		public async Task Create(CatalogEntity catalog)=> await _catalogRepository.Create(catalog);
		
		public async Task<bool> Delete(int id)
		{
			CatalogEntity dbRecord = await _catalogRepository.Table
				.Include(c => c.Books)
				.FirstOrDefaultAsync(b => b.Id == id);
			if (dbRecord == null)
			{
				return false;
			}
			await _catalogRepository.Delete(dbRecord);
			return true;
		}
		public  List<CatalogEntity> GetAll()
		{
			List<CatalogEntity> dbRecord = _catalogRepository.Table
				 .Include(c => c.Books)
				 .ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<CatalogEntity> GetById(int id)
		{
			CatalogEntity dbRecord = await _catalogRepository.Table
				.Where(catalog => catalog.Id == id)
				.Include(c => c.Books)
				.FirstOrDefaultAsync();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<bool> Update(CatalogEntity catalog)
		{
			try
			{
				CatalogEntity dbRecord = await _catalogRepository.Table
					.Where(catalogus => catalogus.Id == catalog.Id)
					.Include(c => c.Books)
					.FirstOrDefaultAsync();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.Name = catalog.Name;
				dbRecord.Books = catalog.Books;

				await _catalogRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<List<CatalogEntity>> GetCatalogsFromFIle() { return null; }
		//=> JsonConvert.DeserializeObject<List<CatalogEntity>>(File.ReadAllText(Constants.Constants.CATALOG_FILE_PATH));
		public async Task SaveCatalogsToFile(List<CatalogEntity> catalogs) { }
		//=> File.WriteAllText(Constants.Constants.CATALOG_FILE_PATH, JsonConvert.SerializeObject(catalogs));
	}
}
