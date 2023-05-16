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

		public void Create(CatalogEntity catalog)
		{
			_catalogRepository.Create(catalog);
		}
		public bool Delete(int id)
		{
			CatalogEntity dbRecord = _catalogRepository.Table
				.Include(c => c.Books)
				.FirstOrDefault(b => b.Id == id);
			if (dbRecord == null)
			{
				return false;
			}
			_catalogRepository.Remove(dbRecord);
			return true;
		}
		public List<CatalogEntity> GetAll()
		{
			List<CatalogEntity> dbRecord = _catalogRepository.Table
				 .ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public CatalogEntity GetById(int id)
		{
			CatalogEntity dbRecord = _catalogRepository.Table
				.Where(catalog => catalog.Id == id)
				.Include(c => c.Books)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public bool Update(CatalogEntity catalog)
		{
			try
			{
				CatalogEntity dbRecord = _catalogRepository.Table
					.Where(catalogus => catalogus.Id == catalog.Id)
					.Include(c => c.Books)
					.FirstOrDefault();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.Name = catalog.Name;
				dbRecord.Books = catalog.Books;

				_catalogRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public List<CatalogEntity> GetCatalogs() { return null; }
		//=> JsonConvert.DeserializeObject<List<CatalogEntity>>(File.ReadAllText(Constants.Constants.CATALOG_FILE_PATH));
		public void SaveCatalogs(List<CatalogEntity> catalogs) { }
		//=> File.WriteAllText(Constants.Constants.CATALOG_FILE_PATH, JsonConvert.SerializeObject(catalogs));
	}
}
