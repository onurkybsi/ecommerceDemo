using Infrastructure.Data;
using ecommerceDemo.Data.Model;

namespace ecommerceDemo.Data.Repository
{
    public class OrderRepository : MongoDBCollectionBase<Order>, IOrderRepository
    {
        public OrderRepository(MongoDBCollectionSettings settings) : base(settings) { }
    }
}