using Ecommerce.Entities;
using Ecommerce.Entities.Helper;
using Ecommerce.Entities.ViewModel;
using ECommerce.API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionsController : Controller
    {
        private readonly ILogger<PurchaseOrdersController> _logger;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(
            ILogger<PurchaseOrdersController> logger,
            ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Client,Admin,SuperAdmin")]
        public async Task<ActionResult<PurchaseOrder>> GetById(int id, CancellationToken cancellationToken)
        {
            
                var result = await _transactionRepository.GetByIdAsync(cancellationToken, id);
                if (result == null)
                    return Ok(new ApiResult
                    {
                        Code = ResultCode.NotFound
                    });

                return Ok(new ApiResult
                {
                    Code = ResultCode.Success,
                    ReturnData = result
                });
        }

        [HttpGet]
        [Authorize(Roles = "Client,Admin,SuperAdmin")]
       public async Task<IActionResult> Get([FromQuery] TransactionFiltreViewModel transactionFiltreViewModel,
           CancellationToken cancellationToken)
       {
           
               if (string.IsNullOrEmpty(transactionFiltreViewModel.PaginationParameters.Search)) transactionFiltreViewModel.PaginationParameters.Search = "";
               var entity = await _transactionRepository.Search(transactionFiltreViewModel, cancellationToken);
               var paginationDetails = new PaginationDetails
               {
                   TotalCount = entity.TotalCount,
                   PageSize = entity.PageSize,
                   CurrentPage = entity.CurrentPage,
                   TotalPages = entity.TotalPages,
                   HasNext = entity.HasNext,
                   HasPrevious = entity.HasPrevious,
                   Search = transactionFiltreViewModel.PaginationParameters.Search
               };

               return Ok(new ApiResult
               {
                   PaginationDetails = paginationDetails,
                   Code = ResultCode.Success,
                   ReturnData = entity
               });
       }

       [HttpGet]
       [Authorize(Roles = "Client,Admin,SuperAdmin")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
       {
           
               return Ok(new ApiResult
               {
                   Code = ResultCode.Success,
                   ReturnData = await _transactionRepository.GetAll(cancellationToken)
               });
       }

    }
}
