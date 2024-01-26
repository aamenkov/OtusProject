using Customer.WebApi.DB;
using Customer.WebApi.Exceptions;
using Customer.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.WebApi.Services
{
    public class CustomerService
    {
        private readonly CustomerContext _dbContext;

        /// <summary>
        /// Базовый конструктор
        /// </summary>
        /// <param name="dbContext"></param>
        public CustomerService(ConfigService configService)
        {
            var optionsPostgreSQL = new DbContextOptionsBuilder<CustomerContext>().UseNpgsql(configService.ConnectionStringPostgres).Options;
            _dbContext = new CustomerContext(optionsPostgreSQL);
        }

        #region CRUD - методы 

        /// <summary>
        /// Добавить покупателя
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<long> AddCustomerAsync(CustomerModel customer)
        {
            try
            {
                var res = await GetCustomerByIdAsync(customer.Id);
                if (res != null) { throw new CustomerAlreadyExistException(); }

                var entity = new CustomerEntity
                {
                    Id = customer.Id,
                    LastName = customer.LastName,
                    FirstName = customer.FirstName
                };

                await _dbContext.Customers.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Получить покупателя 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<CustomerModel> GetCustomerByIdAsync(long customerId)
        {
            try
            {
                var app = await PrepareQuery(x => x.Id == customerId).FirstOrDefaultAsync();
                if (app == null) { return null; }

                var appInfo = ConvertToServiceModel(app);
                return appInfo;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Получить список всех покупателей
        /// </summary>
        /// <returns></returns>
        public async Task<List<CustomerModel>> GetAllCustomersAsync()
        {
            try
            {
                var list = await _dbContext.Customers.ToListAsync();
                return list.Select(x => ConvertToServiceModel(x)).ToList(); 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Удалить покупателя
        /// </summary>
        /// <returns></returns>
        public async Task DeleteCustomerByIdAsync(long customerId)
        {
            try
            {
                var customer = await PrepareQuery(x => x.Id == customerId).FirstOrDefaultAsync();
                if (customer == null) { return; }
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        private IQueryable<CustomerEntity> PrepareQuery(System.Linq.Expressions.Expression<Func<CustomerEntity, bool>> filter)
        {
            return
                _dbContext
                .Customers
                .Where(filter);
        }

        /// <summary>
        /// Конвертация из модели БД в сервисную модель 
        /// </summary>
        /// <param name="appVersion"></param>
        /// <returns></returns>
        public static CustomerModel ConvertToServiceModel(CustomerEntity entity)
        {
            return new CustomerModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };
        }
    }


}
