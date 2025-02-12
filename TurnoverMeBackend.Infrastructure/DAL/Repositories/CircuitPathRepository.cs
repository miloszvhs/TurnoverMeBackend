using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Infrastructure.DAL.Repositories;

public class CircuitPathRepository(TurnoverMeDbContext context) : ICircuitPathRepository
{
    private readonly DbSet<Workflow> _circuitPaths = context.Workflows;
    
    public Workflow GetById(string id)
    {
        return _circuitPaths
            .Include(x => x.Stages)
            .SingleOrDefault(x => x.Id == id);
    }

    public Workflow[] GetAll()
    {
        return _circuitPaths
            .Include(x => x.Stages)
            .ToArray();
    }

    public void Save(Workflow workflow)
    {
        _circuitPaths.Add(workflow);
        context.SaveChanges();
    }
}