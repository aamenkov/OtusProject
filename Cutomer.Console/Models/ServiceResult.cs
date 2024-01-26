using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cutomer.Console.Models
{
    public class ServiceResult<T>
    {
        public T Value { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Message { get; set; }

        public ServiceResult(T value, HttpStatusCode code, string message = "")
        {
            Value = value;
            HttpStatusCode = code;
            Message = message;
        }
    }
}
