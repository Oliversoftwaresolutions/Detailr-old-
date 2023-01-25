using System.Net;

namespace Detailr.Application.Exceptions
{
    public class PostcodesIOEmptyResponseException : Exception 
    {
        public PostcodesIOEmptyResponseException(HttpStatusCode statusCode) 
            :base ($"No response was provided; HttpStatus: {(int)statusCode}") { }
    }
}
