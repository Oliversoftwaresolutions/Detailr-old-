using Detailr.MVC.Contracts;
using MarkEmbling.PostcodesIO;
using MarkEmbling.PostcodesIO.Results;

namespace Detailr.MVC.Services
{
    public class PostcodeService : IPostcodeService
    {
        private readonly IPostcodesIOClient _client;

        public PostcodeService(IPostcodesIOClient client)
        {
            _client = client;
        }

        public Task<PostcodeResult> GetLatLonOfUserLocation(string postcode)
        {
            var business = _client.LookupAsync(postcode);
            return business;
        }

        public Task<bool> ValidatePostcode(string postcode) 
        {
            var isPostcodeValid = _client.ValidateAsync(postcode);
            return isPostcodeValid;
        }
    }
}
