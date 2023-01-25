using AutoMapper;
using Detailr.MVC.Contracts;
using Detailr.MVC.Helpers;
using Detailr.MVC.Helpers.Extensions;
using Detailr.MVC.Models;
using Detailr.MVC.Services.Base;
using MarkEmbling.PostcodesIO.Results;
using Microsoft.AspNetCore.Mvc;

namespace Detailr.MVC.Controllers
{
    public class BusinessController : Controller
    {
        private readonly IBusinessService _businessService;
        private readonly IPostcodeService _postcodeService;
        private readonly IMapper _mapper;

        public BusinessController(IBusinessService businessService, IPostcodeService postcodeService, IMapper mapper)
        {
            _businessService = businessService;
            _postcodeService = postcodeService;
            _mapper = mapper;
        }

        // GET: BusinessController
        public async Task<IActionResult> Index()
        {
            var model = await _businessService.GetBusinesses();
            return View();
        }

        public async Task<List<BusinessVM>> Search(string postcode, int radius)
        {

            // Return null, which tells the client side to show error toast.
            if (postcode == null || radius == 0)
                throw new Exception("Oops, Something went wrong. Please try again.");

            // Validate user input
            var isPostcodeValid = _postcodeService.ValidatePostcode(postcode).Result;
            var businessesInRange = new List<BusinessVM>();

            if (isPostcodeValid)
            {
                // Get LatLon from user input.
                var userLocation = await _postcodeService.GetLatLonOfUserLocation(postcode);
                businessesInRange = await GetAllBusinessesWithinRange(userLocation, radius);
            }

            return businessesInRange;
        }

        public async Task<List<BusinessVM>> GetAllBusinessesWithinRange(PostcodeResult userLocation, int radius)
        {
            var allBusinessesWithinRange = new List<BusinessDto>();

            // Get All Businesses and filter all that are in range
            var businesses = await _businessService.GetBusinesses();

            // Check each business to see if it is in range
            // Add to list if it is in range
            foreach (var business in businesses)
            {

                var distance = new Coordinates(userLocation.Latitude, userLocation.Longitude)
                    .DistanceTo(new Coordinates(business.Latitude, business.Longitude));

                if (distance <= radius)
                {
                    var businessToAdd = _mapper.Map<BusinessDto>(business);

                    allBusinessesWithinRange.Add(businessToAdd);
                }
            }

            return _mapper.Map<List<BusinessVM>>(allBusinessesWithinRange);
        }

        public async Task<PostcodeResult> GetUserLtLon(string postcode)
        {
            return await _postcodeService.GetLatLonOfUserLocation(postcode);
        }

        // GET: BusinssController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model = await _businessService.GetBusinessDetails(id);
            return View(model);
        }

        // GET: BusinessController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: BusinessController/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateBusinessVM business)
        {
            try
            {
                var response = await _businessService.CreateBusiness(business);

                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(business);
        }

        // POST: BusinessController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BusinessVM business)
        {
            try
            {
                var response = await _businessService.UpdateBusiness(id, business);

                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(business);
        }

        // POST: BusinessController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _businessService.DeleteBusiness(id);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

    }
}
