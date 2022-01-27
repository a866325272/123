using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionBackEnd.Data;
using ProductionBackEnd.Interfaces.Repositories.Products;
using ProductionBackEnd.Models.Products;
using ProductionBackEnd.Models.Products.Enums;
using ProductionBackEnd.Repositories.Products.Params;
using ProductionBackEnd.Repositories.Products.Results;
using ProductionBackEnd.Repositories.Utils.Results;
using ProductionBackEnd.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Repositories.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public ProductsRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResult<ProductsListResult>> GetProductsListAsync(GetProductsListParam param)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(param.ProductName))
                query = query.Where(x => x.ProductName.Contains(param.ProductName));

            if (!string.IsNullOrEmpty(param.ProductDescription))
                query = query.Where(x => x.ProductDescription.Contains(param.ProductDescription));

            var total = await query.CountAsync();
            var info = await query.PaginationData(param);

            var result = _mapper.Map<List<ProductsListResult>>(info.Where(x => x.Status.Value != ProductModelStatus.Deleted));

            return new PaginationResult<ProductsListResult>(result, total, param);
        }

        public async Task<ProductsModel> GetProductsByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.CreateUser).Include(x => x.UpdateUser).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InserProductsAsync(ProductsModel param)
        {
            await _context.Products.AddAsync(param);
        }

        public void UpdateProducts(ProductsModel param)
        {
            _context.Entry(param).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
