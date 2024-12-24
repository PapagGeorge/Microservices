using Domain.Models;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderServiceDbContext _context;

        public OrderRepository(OrderServiceDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);

            foreach (var product in order.Products)
            {
                product.OrderId = order.Id;
            }

            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int pageNumber, int pageSize)
        {
            return await _context.Orders
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
