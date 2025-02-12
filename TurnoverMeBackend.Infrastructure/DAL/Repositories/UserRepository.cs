using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Application.Interfaces;
using TurnoverMeBackend.Domain.Entities;

namespace TurnoverMeBackend.Infrastructure.DAL.Repositories;

public class UserRepository(TurnoverMeDbContext dbContext) : IUserRepository
{
    private readonly DbSet<ApplicationUser> _users = dbContext.Users;
    
    public ApplicationUser Get(string id)
    {
        return _users.SingleOrDefault(x => x.Id == id);
    }

    public void SaveOrUpdate(ApplicationUser user)
    {
        dbContext.SaveChanges();
    }
}