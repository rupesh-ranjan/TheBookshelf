using Rupesh.DataAccess.Data;
using Rupesh.DataAccess.Repository.IRepository;
using Rupesh.Models;

namespace Rupesh.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderHeaderRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(OrderHeader orderHeader)
        {
            _dbContext.OrderHeaders.Update(orderHeader);
        }

		public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
		{
			var orderFromDb = _dbContext.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if(!string.IsNullOrEmpty(paymentStatus))
                    orderFromDb.PaymentStatus = paymentStatus;
            }
		}

		public void UpdateStripePaymentId(int id, string sessionId, string paymentIntentId)
		{
			var orderFromDb = _dbContext.OrderHeaders.FirstOrDefault(u => u.Id == id);
			if (!string.IsNullOrEmpty(sessionId))
                orderFromDb.SessionId = sessionId;
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
				orderFromDb.PaymentIntentId = paymentIntentId;
				orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
			}
		}
	}
}
