namespace Entities.Helper;

public class SiteSettings
{
    public CommonSetting? CommonSetting { get; set; }
    public EmailSetting? DefaultEmailSetting { get; set; }
    public IdentitySetting? IdentitySetting { get; set; }
}

public class CommonSetting
{
    public string? ChatHubUrl { get; set; }
    public string? ProtectionSecretKey { get; set; }
}

public class IdentitySetting
{
    public string? IdentitySecretKey { get; set; }
    public string? EncryptKey { get; set; }
    public int NotBeforeMinutes { get; set; }
    public int JwtTtl { get; set; }
    public int RefreshTokenTtl { get; set; }
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public bool PasswordRequireDigit { get; set; }
    public int PasswordRequiredLength { get; set; }
    public bool PasswordRequireNoneAlphanumeric { get; set; }
    public bool PasswordRequireUppercase { get; set; }
    public bool PasswordRequireLowercase { get; set; }
    public bool RequireUniqueEmail { get; set; }
}

public class EmailSetting
{
    public string FromName { get; set; }
    public string FromEmail { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class FrontSetting
{
    public string SiteName { get; set; }
    public string BaseAddress { get; set; }
}