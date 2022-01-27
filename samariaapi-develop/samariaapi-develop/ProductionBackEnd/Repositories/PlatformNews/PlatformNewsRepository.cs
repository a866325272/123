using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductionBackEnd.Data;
using ProductionBackEnd.Interfaces.Repositories.PlatformNews;
using ProductionBackEnd.Models.PlatformNews;
using ProductionBackEnd.Repositories.PlatformNews.Params;
using ProductionBackEnd.Repositories.PlatformNews.Results;
using ProductionBackEnd.Repositories.Utils.Results;
using ProductionBackEnd.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionBackEnd.Repositories.News
{
    public class PlatformNewsRepository : IPlatformNewsRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PlatformNewsRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResult<PlatformNewsListResult>> GetPlatformNewsListAsync(GetPlatformNewsListParam param)
        {
            var query = _context.PlatformNews.AsQueryable();

            if (!string.IsNullOrEmpty(param.Title))
                query = query.Where(x => x.Title.Contains(param.Title));

            if (!string.IsNullOrEmpty(param.Content))
                query = query.Where(x => x.Content.Contains(param.Content));

            var total = await query.CountAsync();
            var info = await query.PaginationData(param);

            var result = _mapper.Map<List<PlatformNewsListResult>>(info);

            return new PaginationResult<PlatformNewsListResult>(result, total, param);
        }

        public async Task<PlatformNewsModel> GetPlatformNewsByIdAsync(int id)
        {
            return await _context.PlatformNews.Include(x => x.CreateUser).Include(x => x.UpdateUser).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task InsertNewsAsync(PlatformNewsModel param)
        {
            await _context.PlatformNews.AddAsync(param);
        }

        public async void UpdatePlatformNews(PlatformNewsModel param)
        {
            _context.Entry(param).State = EntityState.Modified;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
