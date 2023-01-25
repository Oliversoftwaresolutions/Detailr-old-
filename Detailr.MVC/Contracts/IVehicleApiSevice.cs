using Detailr.MVC.Models;

namespace Detailr.MVC.Contracts
{
    public interface IVehicleApiService
    {
        Task <VehicleVM> GetVehicleDetails(string vrn);
    }
}
