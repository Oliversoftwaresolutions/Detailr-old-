using Detailr.Application.DTOs.Business;
using MediatR;

namespace Detailr.Application.Features.Business.Requests.Commands
{
    public class UpdateBusinessCommand : IRequest<Unit>
    {
        public UpdateBusinessDto UpdateBusinessDto { get; set; }
    }
}
