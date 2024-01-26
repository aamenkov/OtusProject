using Cutomer.Console.Models;
using Newtonsoft.Json;
using RestSharp;

namespace Cutomer.Console.Client
{
    public class ServiceClient
    {
        public string BaseUrl { get; set; }
        private RestClient _client;
        public ServiceClient(string baseUrl)
        {
            BaseUrl = baseUrl;
            _client = new RestClient(BaseUrl);
        }

        /// <summary>
        /// Получение покупателя
        /// </summary>
        /// <returns></returns>
        public ServiceResult<CustomerModel> GetCustomerAsync(int id)
        {
            var request = new RestRequest("Customers/ById/{customerId}", Method.Get)
                            .AddUrlSegment("customerId", id);
            request.Timeout = 10000;
            var response = _client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ServiceResult<CustomerModel>(JsonConvert.DeserializeObject<CustomerModel>(response.Content), response.StatusCode);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return new ServiceResult<CustomerModel>(null, response.StatusCode, $"Ошибка сервиса! {response.StatusCode} - Пользователь не найден. {Environment.NewLine} {response.ErrorException} ");
            }

            return new ServiceResult<CustomerModel>(null, response.StatusCode, $"Ошибка сервиса! {Environment.NewLine} {response.ErrorException} ");
        }

        ///// <summary>
        ///// Создание нового покупателя
        ///// </summary>
        ///// <returns></returns>
        public ServiceResult<int> AddCustomerAsync(int id, string name, string surname)
        {
            var request = new RestRequest("Customers/Add", Method.Post)
                .AddParameter("Id", id)
                .AddParameter("FirstName", name)
                .AddParameter("LastName", surname);

            request.Timeout = 10000;
            var response = _client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return new ServiceResult<int>(JsonConvert.DeserializeObject<int>(response.Content), response.StatusCode);
            }

            return new ServiceResult<int>(0, response.StatusCode, $"Ошибка сервиса! {Environment.NewLine} {response.ErrorException} ");
        }
    }
}
