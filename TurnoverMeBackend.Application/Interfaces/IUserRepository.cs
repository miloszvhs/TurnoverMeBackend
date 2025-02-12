using TurnoverMeBackend.Domain.Entities;

namespace TurnoverMeBackend.Application.Interfaces;

public interface IUserRepository
{
    ApplicationUser Get(string id);
    void SaveOrUpdate(ApplicationUser user);
}