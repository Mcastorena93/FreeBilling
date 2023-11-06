using FreeBilling.Data.Entities;

namespace FreeBilling.Web.Data;

public interface IBillingRepository
{
    Task<IEnumerable<Customer>> GetCustomers();
    Task<IEnumerable<Customer>> GetCustomersWithAddresses();
    Task<Customer?>GetCustomer(int id);
    Task<IEnumerable<Employee>> GetEmployees();

    Task<TimeBill?> GetTimeBill(int id);
    void AddEntity<T>(T entity) where T : notnull;

    Task<bool> SaveChanges();
}