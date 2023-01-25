using Detailr.Domain;
using MarkEmbling.PostcodesIO.Results;

namespace Detailr.Application.Contracts.Persistence
{
    public interface IBusinessRepository : IGenericRepository<Business>
    {
        Task<bool> DoesBusinessNameExist(string name);
    }
}
