using FreeBilling.Data.Entities;

namespace FreeBilling.Web.Data;

public interface IBillingRepository
{

    Task<IEnumerable<Employee>> GetEmployees();

    Task<IEnumerable<Customer>> GetCustomers();
    Task<IEnumerable<Customer>> GetCustomersWithAddresses();
    Task<Customer?>GetCustomer(int id);

    Task<TimeBill?> GetTimeBill(int id);
    Task<IEnumerable<TimeBill>> GetTimeBillsForCustomer(int id);
    Task<IEnumerable<TimeBill>> GetTimeBillForCustomer(int id, int billId);
    void AddEntity<T>(T entity) where T : notnull;

    Task<bool> SaveChanges();
}