using MediatR;

namespace Detailr.Application.Features.Business.Requests.Commands
{
    public class DeleteBusinessCommand : IRequest
    {
        public int Id { get; set; }
    }
}
