namespace ECommerce.ControllersTests.BaseContext
{
    public class AuthorizationContext
    {
        public Actor Actor { get; set; }
        public Guid? PersonId { get; set; }
        public Guid? OrganizationId { get; set; }
        public OrganizationalRole? OrganizationalRole { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public string TenantId { get; set; }
        public string ClientId { get; set; }
    }
}
