using Detailr.MVC.Models;
using Detailr.MVC.Services.Base;
using MarkEmbling.PostcodesIO.Results;

namespace Detailr.MVC.Contracts
{
    public interface IBusinessService
    {
        Task<List<BusinessVM>> GetBusinesses();
        Task<BusinessVM> GetBusinessDetails(int id);
        Task<Response<int>> CreateBusiness(CreateBusinessVM business);
        Task<Response<int>> UpdateBusiness(int id, BusinessVM business);
        Task<Response<int>> DeleteBusiness(int id);

    }
    
}
