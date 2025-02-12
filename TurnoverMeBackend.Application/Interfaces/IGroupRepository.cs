using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Application.Interfaces;

public interface IGroupRepository
{
    void Save(Group group);
    Group Get(string id);
    Group[] GetAll();
}