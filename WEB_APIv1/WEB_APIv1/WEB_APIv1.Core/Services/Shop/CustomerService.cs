// ---------------------------------------
// ---
// ---
// ---
// ---------------------------------------

using Microsoft.EntityFrameworkCore;
using WEB_APIv1.Core.DTOs;
using WEB_APIv1.Core.Infrastructure;
using WEB_APIv1.Core.Models.Shop;

namespace WEB_APIv1.Core.Services.Shop
{
    public class CustomerService(ApplicationDbContext dbContext) : ICustomerService
    {
        public IEnumerable<Customer> GetTopActiveCustomers(int count) => throw new NotImplementedException();

        public IEnumerable<Customer> GetAllCustomersData() => dbContext.Customers
               
                .ToList();
        public IEnumerable<CustomerOrderPredictionDto> GetCustomerPredictedOrders()
        {
            var orders = dbContext.Orders
                .Where(o => o.CustId != null)
                .ToList() // Traemos los datos a memoria
                .GroupBy(o => o.CustId) // Agrupar por cliente
                .Select(g =>
                {
                    var lastOrderDate = g.Max(o => o.OrderDate); // Última orden realizada

                    var avgDaysBetweenOrders = g.OrderBy(o => o.OrderDate)
                        .Select((o, index) => new { Order = o, Index = index })
                        .Where(x => x.Index > 0) // Excluir la primera orden
                        .Select(x => (x.Order.OrderDate - g.ElementAt(x.Index - 1).OrderDate).TotalDays)
                        .DefaultIfEmpty(0) // Evitar errores si solo hay una orden
                        .Average(); // Obtener el promedio de días entre órdenes

                    return new CustomerOrderPredictionDto
                    {
                        CustomerName = g.First().Customer.ContactName, // Nombre del cliente
                        LastOrderDate = lastOrderDate,
                        NextPredictedOrder = lastOrderDate.AddDays(avgDaysBetweenOrders) // Sumar el promedio
                    };
                })
                .ToList();

            return orders;
        }

       

        public Customer? GetCustomerById(int customerId)
        {
            return dbContext.Customers
                .FirstOrDefault(c => c.CustId == customerId);
        }

        public void AddCustomer(Customer customer)
        {
            dbContext.Customers.Add(customer);
            dbContext.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            dbContext.Customers.Update(customer);
            dbContext.SaveChanges();
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = dbContext.Customers.FirstOrDefault(c => c.CustId == customerId);
            if (customer != null)
            {
                dbContext.Customers.Remove(customer);
                dbContext.SaveChanges();
            }
        }
    }
}
