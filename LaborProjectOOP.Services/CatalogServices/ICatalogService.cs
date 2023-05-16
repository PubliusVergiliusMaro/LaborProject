using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.CatalogServices
{
	public interface ICatalogService
	{
		void Create(CatalogEntity catalog);
		bool Delete(int id);
		List<CatalogEntity> GetAll();
		CatalogEntity GetById(int id);
		bool Update(CatalogEntity catalog);
		List<CatalogEntity> GetCatalogs();
		void SaveCatalogs(List<CatalogEntity> catalogs);
	}
}
