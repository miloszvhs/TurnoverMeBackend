using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Domain.Entities.MainFlow;

namespace TurnoverMeBackend.Infrastructure.DAL.Repositories;

public class GroupRepository(TurnoverMeDbContext context) : IGroupRepository
{
    private readonly DbSet<Group> _groups = context.Groups;
    
    public void Save(Group group)
    {
        _groups.Add(group);
        context.SaveChanges();
    }

    public Group Get(string id)
    {
        return _groups
            .Include(x => x.Users)
            .SingleOrDefault(x => x.Id == id);
    }

    public Group[] GetAll()
    {
        return _groups.Include(x => x.Users).ToArray();
    }
}