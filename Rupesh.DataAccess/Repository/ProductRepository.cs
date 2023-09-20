using Rupesh.DataAccess.Data;
using Rupesh.DataAccess.Repository.IRepository;
using Rupesh.Models;

namespace Rupesh.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Product product)
        {
            _dbContext.Products.Update(product);
        }
    }
}
