using MarkEmbling.PostcodesIO.Results;

namespace Detailr.MVC.Contracts
{
    public interface IPostcodeService
    {
        Task<PostcodeResult> GetLatLonOfUserLocation(string postcode);
        Task<bool> ValidatePostcode(string postcode);
    }
}
