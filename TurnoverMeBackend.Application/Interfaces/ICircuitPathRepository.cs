using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Application.Interfaces;

public interface ICircuitPathRepository
{
    Workflow GetById(string id);
    Workflow[] GetAll();
    void Save(Workflow workflow);
}