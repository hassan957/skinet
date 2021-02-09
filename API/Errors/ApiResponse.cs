using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode,string message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }


        public int StatusCode { get; }
        public string Message { get; }
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch { 
                400=>"You have made bad request",
                401=>"Authorized, You are not",
                404=>"Resource found,It was not",
                500=>"Error are the path to dark side",
                _=>null

            };
        }
    }
}
