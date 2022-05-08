using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Helper
{
    public class JwtConfigurationModel
    {
        public string? SecretKey { get; set; }
        public string? EncryptKey { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int JwtTTL { get; set; } // hours
        public int RefreshTokenTTL { get; set; } // days
    }
}
