﻿using Ecommerce.Entities.HolooEntity;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services;

public class HolooAccountNumberService : EntityService<HolooAccountNumber>, IHolooAccountNumberService
{
    private const string Url = "api/PaymentMethods/HolooAccount";

    public HolooAccountNumberService(IHttpService http) : base(http)
    {
    }

    public List<HolooAccountNumber> HolooAccountNumbers { get; set; }

    public async Task Load()
    {
        HolooAccountNumbers = (await ReadList(Url)).ReturnData;
    }
}