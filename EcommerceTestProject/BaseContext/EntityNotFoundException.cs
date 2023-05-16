namespace ECommerce.ControllersTests.BaseContext
{
    public class EntityNotFoundException : KnownException
    {
        public Type? EntityType { get; private set; }
        public string? EntityId { get; private set; }

        public EntityNotFoundException(Type entityType, string id)
            : this($"The entity '{entityType.Name}' with ID '{id}' was not found.")
        {
            EntityType = entityType;
            EntityId = id;
        }

        public EntityNotFoundException(string message)
            : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public override string GetErrorCode()
        {
            return KnownErrorCodes.ENTITY_NOT_FOUND;
        }

        public override object GetErrorContextOrDefault()
        {
            if (EntityType != default && EntityId != default)
            {
                return new
                {
                    EntityTypeName = EntityType?.Name,
                    EntityId
                };
            }
            return default;
        }
    }
}
