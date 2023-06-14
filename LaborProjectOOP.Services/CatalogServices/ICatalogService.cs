using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.CatalogServices
{
	public interface ICatalogService
	{
		Task Create(CatalogEntity catalog);
		Task<bool> Delete(int id);
		List<CatalogEntity> GetAll();
		Task<CatalogEntity> GetById(int id);
		Task<bool> Update(CatalogEntity catalog);
		Task<List<CatalogEntity>> GetCatalogsFromFIle();
		Task SaveCatalogsToFile(List<CatalogEntity> catalogs);
	}
}
