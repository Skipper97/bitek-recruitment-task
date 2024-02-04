using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruitmentTask.Api.Models;
using RecruitmentTask.Api.Services;
using RecruitmentTask.Data.DbContexts;
using System.Globalization;

namespace RecruitmentTask.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // DI
        private readonly RecruitmentTaskDbContext _dbContext; // EF DbContext
        private readonly IMapper _mapper; // AutoMapper
        private readonly IProductService _productService;

        public ProductsController(RecruitmentTaskDbContext context, IMapper mapper, IProductService productService)
        {
            _dbContext = context;
            _mapper = mapper;
            _productService = productService;
        }


        [HttpGet("RefreshData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> LoadDataToDatabase()
        {
            await _productService.ReadProductFilesToDatabase();

            return Ok();
        }

        [HttpGet("GetProductInfo/{sku}")]
        [ProducesResponseType(typeof(ProductInfo), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductInfo> GetProductInfo([FromRoute] string sku)
        {
            var result = _productService.GetProductInfoBySku(sku);

            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

    }
}
