﻿using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.Services.IServices;

namespace ECommerce.Services.Services
{
    public class PurchaseOrderService : EntityService<PurchaseOrder>, IPurchaseOrderService
    {
        private const string Url = "api/PurchaseOrders";
        private readonly ICookieService _cookieService;
        private readonly IHttpService _http;

        public PurchaseOrderService(IHttpService http, ICookieService cookieService) : base(http)
        {
            _cookieService = cookieService;
            _http = http;
        }

        public async Task<ServiceResult<PurchaseOrder>> GetByUserId()
        {
            var currentUser = _cookieService.GetCurrentUser();
            if (currentUser.Id == 0)
                return new ServiceResult<PurchaseOrder>
                {
                    Code = ServiceCode.Error
                };
            var result = await Read(Url, $"GetByUserId?userId={currentUser.Id}");
            return Return(result);
        }
        public async Task<ServiceResult<PurchaseOrder>> GetByOrderId(long orderId)
        {
            var result = await Read(Url, $"GetByOrderId?orderId={orderId}");
            return Return(result);
        }
        public async Task<ServiceResult<PurchaseOrder>> GetByUserAndOrderId(long orderId)
        {
            var currentUser = _cookieService.GetCurrentUser();
            if (currentUser.Id == 0)
            {
                return new ServiceResult<PurchaseOrder>
                {
                    Code = ServiceCode.Error
                };
            }
            var result = await Read(Url, $"GetByUserAndOrderId?userId={currentUser.Id}&orderId={orderId}");
            return Return(result);
        }
        public async Task<ServiceResult<PurchaseOrder>> GetPurchaseOrderWithIncludeById(int id)
        {
            var result = await _http.GetAsync<PurchaseOrder>(Url, $"GetPurchaseOrderWithIncludeById?id={id}");
            return Return(result);
        }


        public async Task<ServiceResult> Pay(PurchaseOrder purchaseOrder)
        {
            var result = await Update(Url, purchaseOrder, "Pay");
            return Return(result);
        }

        public Task<ServiceResult> Add(PurchaseOrder purchaseOrder)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> Edit(PurchaseOrder purchaseOrder)
        {
            var result = await Update(Url, purchaseOrder);
            return Return(result);
        }


        public async Task<ServiceResult<List<PurchaseListViewModel>>> PurchaseList(int userId = 0, string search = "",
         int pageNumber = 0, int pageSize = 10, int purchaseSort = 1, bool? isPaied = null, DateTime? fromCreationDate = null,
          DateTime? toCreationDate = null, int? statusId = null, decimal? minimumAmount = null, decimal? maximumAmount = null,
          PaymentMethodStatus? paymentMethodStatus = null, long orderId = 0)
        {
            //var result = await _http.GetAsync<List<ProductIndexPageViewModel>>(Url, $"NewProducts?count={count}");
            //return Return<List<ProductIndexPageViewModel>>(result);

            var command = "Get?" +
                          $"PaginationParameters.PageNumber={pageNumber}&" +
                          $"PaginationParameters.PageSize={pageSize}&";
            if (!string.IsNullOrEmpty(search)) command += $"PaginationParameters.Search={search}&";
            if (isPaied != null) command += $"IsPaied={isPaied}&";
            if (userId > 0) command += $"UserId={userId}&";
            if (fromCreationDate != null) command += $"FromCreationDate={fromCreationDate}&";
            if (toCreationDate != null) command += $"ToCreationDate={toCreationDate}&";
            if (statusId != null) command += $"StatusId={statusId}&";
            if (minimumAmount != null) command += $"MinimumAmount={minimumAmount}&";
            if (maximumAmount != null) command += $"MaximumAmount={maximumAmount}&";
            if (paymentMethodStatus != null) command += $"PaymentMethodStatus={paymentMethodStatus}&";

            command += $"PurchaseSort={purchaseSort}";
            var result = await _http.GetAsync<List<PurchaseListViewModel>>(Url, command);
            return Return(result);
        }

    }
}
