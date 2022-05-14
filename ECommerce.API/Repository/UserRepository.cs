using API.DataContext;
using API.Interface;
using API.Utilities;
using Entities;
using Entities.Helper;
using Microsoft.EntityFrameworkCore;

namespace API.Repository;

public class UserRepository : AsyncRepository<User>, IUserRepository
{
    public UserRepository(SunflowerECommerceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PagedList<User>> Search(PaginationParameters paginationParameters,
        CancellationToken cancellationToken)
    {
        return PagedList<User>.ToPagedList(
            await TableNoTracking.Where(x => x.UserName.Contains(paginationParameters.Search)).AsNoTracking()
                .OrderBy(on => on.Id).ToListAsync(cancellationToken),
            paginationParameters.PageNumber,
            paginationParameters.PageSize);
    }

    public async Task<User> GetByEmailOrUserName(string input, CancellationToken cancellationToken)
    {
        return await TableNoTracking.Where(p => p.Email == input || p.PhoneNumber == input || p.UserName == input)
            .Include(x => x.UserRole).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<User> GetByPhoneNumber(string phone, CancellationToken cancellationToken)
    {
        return await TableNoTracking.Where(p => p.PhoneNumber == phone).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> Exists(int id, string email, string phoneNumber, CancellationToken cancellationToken)
    {
        return await TableNoTracking.AnyAsync(p => p.Id != id && (p.Email == email || p.PhoneNumber == phoneNumber),
            cancellationToken);
    }

    public async Task<List<UserRole>> GetUserRoles(int id, CancellationToken cancellationToken)
    {
        var userRoles = await DbContext.UserRoles.Where(q => q.UserId == id).Select(p => p.RoleId)
            .ToListAsync(cancellationToken);
        return await DbContext.Roles.Where(p => userRoles.Contains(p.Id)).ToListAsync(cancellationToken);
    }

    public async Task<List<UserRole>> GetApplicationRoles(CancellationToken cancellationToken)
    {
        return await DbContext.Roles.ToListAsync(cancellationToken);
    }

    public async Task AddLoginHistory(int userId, string token, string ipAddress, DateTime expirationDate)
    {
        await DbContext.LoginHistories.AddAsync(new LoginHistory
        {
            CreationDate = DateTime.Now,
            ExpirationDate = expirationDate,
            IpAddress = ipAddress,
            Token = token,
            UserId = userId
        });
        await DbContext.SaveChangesAsync();
    }
}