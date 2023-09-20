using Rupesh.DataAccess.Data;
using Rupesh.DataAccess.Repository.IRepository;
using Rupesh.Models;

namespace Rupesh.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Category category)
        {
            _dbContext.Categories.Update(category);
        }
    }
}
