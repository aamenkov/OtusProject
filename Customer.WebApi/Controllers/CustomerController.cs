using Customer.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Customer.WebApi.Models;

namespace Customer.WebApi.Controllers
{
    public class CustomerController : Controller
    {
        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public static CustomerService _customerService;

        /// <summary>
        /// Получить список покупателей
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<List<CustomerModel>>> GetCustomerListAsync()
        {
            try
            {
                var list = await _customerService.GetAllCustomersAsync();
                return Ok(list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Добавить нового покупателя
        /// </summary>
        /// <param name="customer">Покупатель</param>
        /// <returns></returns>
        [HttpPost("[controller]/Add")]
        public async Task<ActionResult<int>> AddCustomerAsync(CustomerModel customer)
        {
            try
            {
                var customerId = await _customerService.AddCustomerAsync(customer);
                return Ok(customerId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Получить инфо о покупателе
        /// </summary>
        /// <param name="customerId">Идентификатор покупателя</param>
        /// <returns></returns>
        [HttpGet("[controller]/{customerId}")]
        public async Task<ActionResult<CustomerModel>> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                return await FindEntity(_customerService.GetCustomerByIdAsync(customerId));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Удалить покупателя
        /// </summary>
        /// <param name="customerId">Идентификатор покупателя</param>
        /// <returns></returns>
        [HttpDelete("[controller]/{customerId}")]
        [Authorize]
        public async Task<ActionResult<bool>> DeleteCustomerByIdAsync(int customerId)
        {
            try
            {
                await _customerService.DeleteCustomerByIdAsync(customerId);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Problem(e.Message);
            }
        }

        /// <summary>
        /// Удалить покупателя
        /// </summary>
        /// <param name="customerId">Идентификатор покупателя</param>
        /// <returns></returns>
        [HttpGet("[controller]/GetAll")]
        [Authorize]
        public async Task<ActionResult<bool>> GetAllCustomersAsync(int customerId)
        {
            try
            {
                await _customerService.GetAllCustomersAsync();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Problem(e.Message);
            }
        }

        private async Task<ActionResult<T>> FindEntity<T>(Task<T> task)
        {
            var res = await task;
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }
    }
}
