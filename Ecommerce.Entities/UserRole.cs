using Microsoft.AspNetCore.Identity;

namespace Entities;

public class UserRole : IdentityRole<int>, IBaseEntity<int>
{
}