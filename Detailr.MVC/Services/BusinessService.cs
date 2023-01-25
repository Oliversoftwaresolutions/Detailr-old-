using AutoMapper;
using Detailr.MVC.Contracts;
using Detailr.MVC.Models;
using Detailr.MVC.Services.Base;

namespace Detailr.MVC.Services
{
    public class BusinessService : BaseHttpService, IBusinessService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _client;

        public BusinessService(ILocalStorageService localStorageService, IMapper mapper, IClient client) : base(localStorageService, client)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _client = client;
        }

        /// <summary>
        /// Create new Business
        /// </summary>
        /// <param name="business">Business details</param>
        /// <returns>Response<int></returns>
        public async Task<Response<int>> CreateBusiness(CreateBusinessVM business)
        {
            try
            {
                var response = new Response<int>();
                CreateBusinessDto createBusiness = _mapper.Map<CreateBusinessDto>(business);
                AddBearerToken();
                var apiResponse = await _client.BusinessPOSTAsync(createBusiness);

                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        /// <summary>
        ///  Delete Business
        /// </summary>
        /// <param name="id">Id of business to delete</param>
        /// <returns>Response<int></returns>
        public async Task<Response<int>> DeleteBusiness(int id)
        {
            try
            {
                AddBearerToken();

                await _client.BusinessDELETEAsync(id);
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
         }

        /// <summary>
        ///  Get Business Details
        /// </summary>
        /// <param name="id">Id of business</param>
        /// <returns>BusinessVM</returns>
        public async Task<BusinessVM> GetBusinessDetails(int id)
        {
            AddBearerToken();
            var business = await _client.BusinessGETAsync(id);
            return _mapper.Map<BusinessVM>(business);
        }

        /// <summary>
        /// Get all businesses
        /// </summary>
        /// <returns>List<BusinessVM></returns>
        public async Task<List<BusinessVM>> GetBusinesses()
        {
            AddBearerToken();
            var businesses = await _client.BusinessAllAsync();
            return _mapper.Map<List<BusinessVM>>(businesses);
        }

        /// <summary>
        /// Update Business
        /// </summary>
        /// <param name="id">Id of business</param>
        /// <param name="business">new business details</param>
        /// <returns>Response<int></returns>
        public async Task<Response<int>> UpdateBusiness(int id, BusinessVM business)
        {
            try
            {
                UpdateBusinessDto businessDto = _mapper.Map<UpdateBusinessDto>(business);
                AddBearerToken();
                await _client.BusinessPUTAsync(id.ToString(), businessDto);
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
