// ---------------------------------------
// ---
// ---
// ---
// ---------------------------------------

using WEB_APIv1.Core.DTOs;
using WEB_APIv1.Core.Models.Shop;

namespace WEB_APIv1.Core.Services.Shop
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetTopActiveCustomers(int count);
        IEnumerable<Customer> GetAllCustomersData();
        IEnumerable<CustomerOrderPredictionDto> GetCustomerPredictedOrders();
    }
}
