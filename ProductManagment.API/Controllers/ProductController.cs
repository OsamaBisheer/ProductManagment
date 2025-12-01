using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductManagment.API.Controllers;
using ProductManagment.Domain.Interfaces.IServices;
using ProductManagment.Domain.ViewModels.Common;
using ProductManagment.Domain.ViewModels.Product;
using static ProductManagment.Domain.Enums.Enumeration;

namespace ProductManagment.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : CommonControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet, Route("by-id/{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            (ProductResultVM model, ResponseCodeEnum responseCode) = await productService.GetById(id);

            return GetActionResult(new ResponseModel
            {
                Result = model,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpGet, Route("count")]
        public async Task<ActionResult> GetCount()
        {
            (int count, ResponseCodeEnum responseCode) = await productService.GetCount();

            return GetActionResult(new ResponseModel
            {
                Result = count,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpGet, Route("search")]
        public async Task<ActionResult> GetProductsForPagination([FromQuery] DataTableRequestVM requestVM)
        {
            (DataTableResponseVM<ProductResultVM> dataTableResponseVM, ResponseCodeEnum responseCode) = await productService.Search(requestVM);

            return GetActionResult(new ResponseModel
            {
                Result = dataTableResponseVM,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpPost, Route("create")]
        public async Task<ActionResult> Create(ProductCreateVM createVM)
        {
            if (!ModelState.IsValid)
            {
                return GetActionResult(new ResponseModel
                {
                    Result = 0,
                    Code = ResponseCodeEnum.BadRequest,
                    MessageFL = nameof(ResponseCodeEnum.BadRequest),
                    MessageSL = nameof(ResponseCodeEnum.BadRequest)
                });
            }

            createVM.CreatedByUserId = GetCurrentUserId();
            (int id, ResponseCodeEnum responseCode) = await productService.Create(createVM);

            return GetActionResult(new ResponseModel
            {
                Result = id,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpPut, Route("update")]
        public async Task<ActionResult> Update(ProductUpdateVM updateVM)
        {
            if (!ModelState.IsValid)
            {
                return GetActionResult(new ResponseModel
                {
                    Result = 0,
                    Code = ResponseCodeEnum.BadRequest,
                    MessageFL = nameof(ResponseCodeEnum.BadRequest),
                    MessageSL = nameof(ResponseCodeEnum.BadRequest)
                });
            }

            (int id, ResponseCodeEnum responseCode) = await productService.Update(updateVM);

            return GetActionResult(new ResponseModel
            {
                Result = id,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }

        [HttpDelete, Route("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return GetActionResult(new ResponseModel
                {
                    Result = 0,
                    Code = ResponseCodeEnum.BadRequest,
                    MessageFL = nameof(ResponseCodeEnum.BadRequest),
                    MessageSL = nameof(ResponseCodeEnum.BadRequest)
                });
            }

            var currentUserId = GetCurrentUserId();
            ResponseCodeEnum responseCode = await productService.Delete(id, currentUserId);

            return GetActionResult(new ResponseModel
            {
                Result = id,
                Code = responseCode,
                MessageFL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString(),
                MessageSL = responseCode == ResponseCodeEnum.Success ? null : responseCode.ToString()
            });
        }
    }
}