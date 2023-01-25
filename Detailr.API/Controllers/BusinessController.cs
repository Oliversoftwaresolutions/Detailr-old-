using Detailr.Application.DTOs.Business;
using Detailr.Application.Features.Business.Requests.Commands;
using Detailr.Application.Features.Business.Requests.Queries;
using Detailr.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Detailr.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///  Get Businesses
        /// </summary>
        /// <returns>List<BusinessDto></returns>
        [HttpGet]
        public async Task<ActionResult<List<BusinessDto>>> Get()
        {
            var businesses = await _mediator.Send(new GetBusinessListRequest());
            return Ok(businesses);
        }

        /// <summary>
        ///  Get Business by Id
        /// </summary>
        /// <param name="id">Id of business</param>
        /// <returns>BusinessDto</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BusinessDto>> Get(int id)
        {
            var business = await _mediator.Send(new GetBusinessDetailRequest { Id = id });
            return Ok(business);
        }

        /// <summary>
        ///  Create Business
        /// </summary>
        /// <param name="business">Business details</param>
        /// <returns>BaseCommandResponse</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBusinessDto business)
        {
            var response = await _mediator.Send(new CreateBusinessCommand { CreateBusinessDto = business });
            return Ok(response);
        }

        /// <summary>
        ///  Update Business
        /// </summary>
        /// <param name="business">Business details</param>
        /// <returns>No Content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put([FromBody] UpdateBusinessDto business)
        {
            await _mediator.Send(new UpdateBusinessCommand { UpdateBusinessDto = business });
            return NoContent();
        }

        /// <summary>
        ///  Delete Business
        /// </summary>
        /// <param name="id">Id of business to delete</param>
        /// <returns>No Content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteBusinessCommand { Id = id });
            return NoContent();
        }
    }
}
