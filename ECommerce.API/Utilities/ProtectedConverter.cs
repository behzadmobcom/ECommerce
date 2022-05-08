using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq.Expressions;
using Entities.Helper;

namespace API.Utilities
{
    public class ProtectedConverter : ValueConverter<string, string>
    {
        class Wrapper
        {
            readonly IDataProtector _dataProtector;

            public Wrapper(IDataProtectionProvider dataProtectionProvider, IConfiguration configRoot)
            {
                var key = configRoot.GetSection(nameof(SiteSettings)).Get<SiteSettings>().CommonSetting.ProtectionSecretKey;
                _dataProtector = dataProtectionProvider.CreateProtector(key);
            }

            public Expression<Func<string, string>> To => x => x != null ? _dataProtector.Protect(x) : null;
            public Expression<Func<string, string>> From => x => x != null ? _dataProtector.Unprotect(x) : null;
        }


        public ProtectedConverter(IDataProtectionProvider provider, IConfiguration configRoot, ConverterMappingHints mappingHints = default)
            : this(new Wrapper(provider, configRoot), mappingHints)
        { }

        ProtectedConverter(Wrapper wrapper, ConverterMappingHints mappingHints)
            : base(wrapper.To, wrapper.From, mappingHints)
        {
        }
    }
}
