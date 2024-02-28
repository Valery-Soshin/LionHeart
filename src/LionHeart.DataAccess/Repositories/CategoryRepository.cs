using LionHeart.Core.Models;
using LionHeart.Core.Repositories;

namespace LionHeart.DataAccess.Repositories;

public class CategoryRepository(ApplicationDbContext dbContext) : RepositoryBase<Category>(dbContext), ICategoryRepository
{

}