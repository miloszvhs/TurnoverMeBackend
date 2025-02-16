using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TurnoverMeBackend.Api.Endpoints;
using TurnoverMeBackend.Domain.Entities;
using TurnoverMeBackend.Domain.Entities.MainFlow;
using TurnoverMeBackend.Infrastructure.DAL;

namespace TurnoverMeBackend.Api.ApiServices;

public class UserService(TurnoverMeDbContext dbContext, UserManager<ApplicationUser> userManager,
    UserGroupRepository userGroupRepository,
    RoleManager<IdentityRole> roleManager)
    : BaseService(dbContext)
{
    public UserResponse[] GetUsers()
    {
        Console.WriteLine(dbContext.Database.CurrentTransaction);

        var users = dbContext.Users.ToListAsync().GetAwaiter().GetResult();
        var newUsers = users.Select(Map);
        return newUsers.ToArray();

        UserResponse Map(ApplicationUser user)
        {
            var role = userManager.GetRolesAsync(user).GetAwaiter().GetResult().SingleOrDefault();
            return new UserResponse()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                GroupName = string.IsNullOrWhiteSpace(user.GroupId) 
                    ? null 
                    : userGroupRepository.Get(user.GroupId)?.Name ?? null,
                GroupId = user.GroupId,
                Role =  role,
                RoleId = roleManager.FindByNameAsync(role).GetAwaiter().GetResult()?.Id
            };
        }
    }
    
    public UserCreated? CreateUser(CreateUser createUser)
    {
        var user = new ApplicationUser
        {
            UserName = createUser.UserName,
            Email = createUser.Email,
            GroupId = createUser.GroupId,
            ForcePasswordChange = createUser.ForcePasswordChange
        };

        var result = userManager.CreateAsync(user, createUser.Password).GetAwaiter().GetResult();
        if (!result.Succeeded)
            throw new Exception("Could not create user");

        var role = roleManager.FindByIdAsync(createUser.RoleId).GetAwaiter().GetResult();
        if (role == null) 
            throw new Exception("Role not found");
        
        var roleResult = userManager.AddToRoleAsync(user, role.Name).GetAwaiter().GetResult();
        if (!roleResult.Succeeded)
            throw new Exception("Could not add role to user");

        return new UserCreated
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            GroupId = user.GroupId,
            Role = role.Name
        };
    }

    public ApplicationUser UpdateUser(string id, UpdateUser updateUser)
    {
        var user = userManager.FindByIdAsync(id).GetAwaiter().GetResult();
        if (user == null)
            throw new Exception("User doesnt exists");

        if (!string.IsNullOrEmpty(updateUser.UserName) && user.UserName != updateUser.UserName)
        {
            user.UserName = updateUser.UserName;
        }

        if (!string.IsNullOrEmpty(updateUser.Email) && user.Email != updateUser.Email)
        {
            user.Email = updateUser.Email;
        }
        
        if (!string.IsNullOrEmpty(updateUser.RoleId))
        {
            var role = roleManager.FindByIdAsync(updateUser.RoleId).GetAwaiter().GetResult();
            if (role == null) 
                throw new Exception("Role not found");
         
            var currentRoles = userManager.GetRolesAsync(user).GetAwaiter().GetResult();
            userManager.RemoveFromRolesAsync(user, currentRoles).GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, role.Name).GetAwaiter().GetResult();
        }

        if (!string.IsNullOrEmpty(updateUser.Password))
        {
            var token = userManager.GeneratePasswordResetTokenAsync(user).GetAwaiter().GetResult();
            var result = userManager.ResetPasswordAsync(user, token, updateUser.Password).GetAwaiter().GetResult();
            if (!result.Succeeded)
                throw new Exception("Update user error");
        }
        
        if (!string.IsNullOrEmpty(updateUser.GroupId) && user.GroupId != updateUser.GroupId)
            user.GroupId = updateUser.GroupId;

        var updateResult = userManager.UpdateAsync(user).GetAwaiter().GetResult();
        if (!updateResult.Succeeded)
            throw new Exception("Update user error");

        dbContext.SaveChangesAsync();
        
        return user;
    }

    public async Task DeleteUser(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user != null)
        {
            await userManager.DeleteAsync(user);
        }
    }

    public class UserCreated
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string GroupId { get; set; }
        public string Role { get; set; }
    }

    public class UserResponse
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? Role { get; set; }
        public string RoleId { get; set; }
    }
}

public class UserGroupRepository(TurnoverMeDbContext context)
{
    public Group Get(string id)
    {
        return context.Groups
            .Include(x => x.Users)
            .SingleOrDefault(x => x.Id == id);
    }
}