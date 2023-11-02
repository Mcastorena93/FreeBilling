using FreeBilling.Data.Entities;
using FreeBilling.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace FreeBilling.Web.Controller
{
    [Route("/api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IBillingRepository _repository;

        public CustomersController(IBillingRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Customer>> Get()
        {
            return await _repository.GetCustomers();
        }
        [HttpGet("{id:int}")]
        public async Task<Customer?> GetOne(int id)
        {
            return await _repository.GetCustomer(id);
        }
    }
}
