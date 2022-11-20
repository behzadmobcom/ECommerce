using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Entities;

public class UserRole : IdentityRole<int>, IBaseEntity<int>
{
}