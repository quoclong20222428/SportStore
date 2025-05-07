
using Microsoft.EntityFrameworkCore;

namespace SportStore.Models
{
    public class EFOrderRepositorycs : IOrderRepository
    {
        private readonly StoreDbContext context;
        public EFOrderRepositorycs(StoreDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders()
        {
            return context.Orders
                .Include(o => o.Lines)
                .ThenInclude(l => l.Product);
        }

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Product));
            if(order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
