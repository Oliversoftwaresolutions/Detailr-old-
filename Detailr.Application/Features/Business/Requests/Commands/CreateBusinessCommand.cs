using Detailr.Application.DTOs.Business;
using Detailr.Application.Responses;
using MediatR;

namespace Detailr.Application.Features.Business.Requests.Commands
{
    public class CreateBusinessCommand : IRequest<BaseCommandResponse>
    {
        public CreateBusinessDto CreateBusinessDto { get; set; }
    }
}
