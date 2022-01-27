using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using ProductionBackEnd.Dtos.PlatformNews.Params;
using ProductionBackEnd.Dtos.PlatformNews.Results;
using ProductionBackEnd.Extensions;
using ProductionBackEnd.Interfaces.Repositories.PlatformNews;
using ProductionBackEnd.Interfaces.Repositories.User;
using ProductionBackEnd.Models.PlatformNews;
using ProductionBackEnd.Repositories.PlatformNews.Results;
using ProductionBackEnd.Repositories.Utils.Results;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProductionBackEnd.Controllers.PlatformNews
{
    /// <summary>
    /// 最新消息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformNewsController : ControllerBase
    {
        private readonly IPlatformNewsRepository _platformNewsRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public PlatformNewsController(IPlatformNewsRepository platformNewsRepository, IUserRepository userRepository, IMapper mapper)
        {
            _platformNewsRepository = platformNewsRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// 取得最新消息列表
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<ActionResult<PaginationResult<PlatformNewsListResult>>> GetPlatformNewsListAsync([FromQuery] GetPlatformNewsListParam param)
        {
            return await _platformNewsRepository.GetPlatformNewsListAsync(
                new Repositories.PlatformNews.Params.GetPlatformNewsListParam()
                {
                    PageNumber = param.PageNumber,
                    PageSize = param.PageSize,
                    Title = param.Title,
                    Content = param.Content
                });
        }

        /// <summary>
        /// 取得最新消息內容
        /// </summary>
        /// <returns></returns>
        [HttpGet("{Id}", Name = "GetPlatformNewsDetail")]
        public async Task<ActionResult<PlatformNewsDetailResult>> GetPlatformNewsDetailAsync(int Id)
        {
            var product = await _platformNewsRepository.GetPlatformNewsByIdAsync(Id);

            return _mapper.Map<PlatformNewsDetailResult>(product);
        }

        /// <summary>
        /// 取得圖片
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}/Image")]
        public async Task<ActionResult> GetPlatformNewsImage(int Id)
        {
            var platformNews = await _platformNewsRepository.GetPlatformNewsByIdAsync(Id);

            if (platformNews == null) return NotFound("找不到資源");

            Stream stream = new MemoryStream(platformNews.TitlePageImg);

            new FileExtensionContentTypeProvider().TryGetContentType(platformNews.TitlePageImgName, out string contentType);

            return File(stream, contentType ?? "application/octet-stream");
        }

        /// <summary>
        /// 新增最新消息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost()]
        public async Task<ActionResult> InsertPlatformNewsAsync([FromForm] InsertPlatformNewsParam param)
        {
            var userId = User.GetUserId();
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null) return Unauthorized();

            var platformNews = _mapper.Map<PlatformNewsModel>(param);
            platformNews.CreateUser = user;
            platformNews.CreateDateTime = DateTime.UtcNow.AddHours(8);
            platformNews.UpdateUser = user;
            platformNews.UpdateDateTime = DateTime.UtcNow.AddHours(8);

            if(param.TitlePageImgFile != null)
            {
                platformNews.TitlePageImgName = $"{Guid.NewGuid()}{Path.GetExtension(param.TitlePageImgFile.FileName)}";

                using (var ms = new MemoryStream())
                {
                    await param.TitlePageImgFile.CopyToAsync(ms);
                    platformNews.TitlePageImg = ms.ToArray();
                }
            }

            await _platformNewsRepository.InsertNewsAsync(platformNews);

            if (!await _platformNewsRepository.SaveAllAsync()) return BadRequest("新增平台消息發生錯誤");

            var result = _mapper.Map<PlatformNewsDetailResult>(platformNews);

            return CreatedAtAction("GetPlatformNewsDetail", new { Id = result.Id }, result);
        }

        /// <summary>
        /// 更新最新消息
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("{Id}")]
        public async Task<ActionResult> UpdatePlatformNewsAsync(int Id, [FromForm] UpdatePlatformNewsParam param)
        {
            var userId = User.GetUserId();
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null) return Unauthorized();

            var platformNews = await _platformNewsRepository.GetPlatformNewsByIdAsync(Id);

            if (platformNews == null) return NotFound("找不到資源");

            _mapper.Map(param, platformNews);
            platformNews.UpdateUser = user;
            platformNews.UpdateDateTime = DateTime.UtcNow.AddHours(8);

            if (param.TitlePageImgFile != null)
            {
                platformNews.TitlePageImgName = $"{Guid.NewGuid()}{Path.GetExtension(param.TitlePageImgFile.FileName)}";

                using (var ms = new MemoryStream())
                {
                    await param.TitlePageImgFile.CopyToAsync(ms);
                    platformNews.TitlePageImg = ms.ToArray();
                }
            }

            _platformNewsRepository.UpdatePlatformNews(platformNews);

            if (!await _platformNewsRepository.SaveAllAsync()) return BadRequest("更新平台消息發生錯誤");

            return NoContent();
        }

        /// <summary>
        /// 刪除最新消息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeletePlatformNewsAsync(int Id)
        {
            var userId = User.GetUserId();
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null) return Unauthorized();

            var platformNews = await _platformNewsRepository.GetPlatformNewsByIdAsync(Id);
            platformNews.Status = Models.PlatformNews.Enums.PlatformNewsModelStatus.Deleted;
            platformNews.UpdateUser = user;
            platformNews.UpdateDateTime = DateTime.UtcNow.AddHours(8);

            _platformNewsRepository.UpdatePlatformNews(platformNews);

            if (!await _platformNewsRepository.SaveAllAsync()) return BadRequest("刪除平台消息發生錯誤");

            return NoContent();
        }
    }
}
