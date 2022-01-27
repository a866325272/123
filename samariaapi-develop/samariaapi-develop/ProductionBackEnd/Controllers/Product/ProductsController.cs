using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using ProductionBackEnd.Dtos.Products.Params;
using ProductionBackEnd.Dtos.Products.Results;
using ProductionBackEnd.Extensions;
using ProductionBackEnd.Interfaces.Repositories.Products;
using ProductionBackEnd.Interfaces.Repositories.User;
using ProductionBackEnd.Models.Products;
using ProductionBackEnd.Repositories.Products.Results;
using ProductionBackEnd.Repositories.Utils.Results;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProductionBackEnd.Controllers.Products
{
    /// <summary>
    /// 商品
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="productsRepository"></param>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
        public ProductsController(IProductsRepository productsRepository, IUserRepository userRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 取得(所有或指定)的商品列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<PaginationResult<ProductsListResult>>> GetProductsListAsync([FromQuery] GetProductsListParam param)
        {
            return await _productsRepository.GetProductsListAsync(
                new Repositories.Products.Params.GetProductsListParam()
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                    ProductName = param.ProductName,
                    ProductDescription = param.ProductDescription,
                });
        }

        /// <summary>
        /// 取得指定商品內容
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}", Name = "GetProductsDetail")]
        public async Task<ActionResult<ProductsDetailResult>> GetProductsDetailAsync(int Id)
        {
            var product = await _productsRepository.GetProductsByIdAsync(Id);

            return _mapper.Map<ProductsDetailResult>(product);
        }

        /// <summary>
        /// 取得指定商品圖片
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}/Image")]
        public async Task<ActionResult> GetProductImage(int Id)
        {
            var products = await _productsRepository.GetProductsByIdAsync(Id);

            if (products == null) return NotFound("找不到資源");

            Stream stream = new MemoryStream(products.ProductImg);

            new FileExtensionContentTypeProvider().TryGetContentType(products.ProductImgName, out string contentType);

            return File(stream, contentType ?? "application/octet-stream");
        }

        /// <summary>
        /// 新增商品資訊
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost()]
        public async Task<ActionResult> InsertProductsAsync([FromForm] InsertProductsParam param)
        {
            var userId = User.GetUserId();
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null) return Unauthorized();

            var products = _mapper.Map<ProductsModel>(param);
            products.CreateUser = user;
            products.CreateDateTime = DateTime.UtcNow.AddHours(8);
            products.UpdateUser = user;
            products.UpdateDateTime = DateTime.UtcNow.AddHours(8);

            if(param.ProductImgFile != null)
            {
                products.ProductImgName = $"{Guid.NewGuid()}{Path.GetExtension(param.ProductImgFile.FileName)}";

                using( var ms = new MemoryStream())
                {
                    await param.ProductImgFile.CopyToAsync(ms);
                    products.ProductImg = ms.ToArray();
                }
            }

            await _productsRepository.InserProductsAsync(products);

            if (!await _productsRepository.SaveAllAsync()) return BadRequest("新增商品資訊發生錯誤");

            var result = _mapper.Map<ProductsDetailResult>(products);

            return CreatedAtAction("GetProductsDetail", new { Id = result.Id }, result);
        }

        /// <summary>
        /// 更新指定商品資訊
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateProductsAsync(int Id, [FromForm] UpdateProductsParam param)
        {
            var userId = User.GetUserId();

            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null) return Unauthorized();

            var products = await _productsRepository.GetProductsByIdAsync(Id);

            if (products == null) return NotFound("找不到資源");

            _mapper.Map(param, products);
            products.UpdateUser = user;
            products.UpdateDateTime = DateTime.UtcNow.AddHours(8);
            
            if(param.ProductImgFile != null)
            {
                products.ProductImgName = $"{Guid.NewGuid()}{Path.GetExtension(param.ProductImgFile.FileName)}";

                using(var ms = new MemoryStream())
                {
                    await param.ProductImgFile.CopyToAsync(ms);
                    products.ProductImg = ms.ToArray();
                }
            }

            _productsRepository.UpdateProducts(products);

            if (!await _productsRepository.SaveAllAsync()) return BadRequest("更新商品資訊發生錯誤");

            return NoContent();
         }

        /// <summary>
        /// 刪除指定商品資訊
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProductsAsync(int Id)
        {
            var userId = User.GetUserId();
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (userId == null) return Unauthorized();

            var products = await _productsRepository.GetProductsByIdAsync(Id);
            products.Status = Models.Products.Enums.ProductModelStatus.Deleted;
            products.UpdateUser = user;
            products.UpdateDateTime = DateTime.UtcNow.AddHours(8);

            _productsRepository.UpdateProducts(products);

            if (!await _productsRepository.SaveAllAsync()) return BadRequest("刪除商品資訊發生錯誤");

            return NoContent();
        }

    }
}
