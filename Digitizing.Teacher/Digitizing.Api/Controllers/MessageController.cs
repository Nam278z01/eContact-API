using System;
using Library.Common.Response;
using Library.Common.Message;
using Library.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Library.DataModel;
using Library.BusinessLogicLayer;
using Microsoft.AspNetCore.Hosting;
using Library.Common.Caching;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Digitizing.Api.Cms.Controllers;
using Easy.Common.Extensions;
using System.IO;
using Digitizing.Api.CustomImport;

namespace Digitizing.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : BaseController
    {
        private IWebHostEnvironment _env;
        private IMessageBusiness _messageBUS;
        public MessageController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IMessageBusiness messageBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _messageBUS = messageBUS;
        }
        [Route("search-list-user")]
        [HttpPost] //Tìm kiếm người dùng trong hệ thống
        public async Task<ResponseListMessage<List<UserInChatModel>>> SearchListUser(UserInChatRequest request)
        {
            var response = new ResponseListMessage<List<UserInChatModel>>();
            try
            {
                request.text_search = request.text_search.IsNullOrEmpty() ? "" : request.text_search;
                request.user_id = null;
                long total = 0;
                var data = await Task.FromResult(_messageBUS.SearchListUser(request, out total));
                response.TotalItems = total;
                response.Data = data;
                response.Page = request.page;
                response.PageSize = request.pageSize;

            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("search-list-teacher")]
        [HttpPost] //Tìm kiếm người dùng trong hệ thống
        public async Task<ResponseListMessage<List<UserInChatModel>>> SearchListTeacher(UserInChatRequest request)
        {
            var response = new ResponseListMessage<List<UserInChatModel>>();
            try
            {
                request.text_search = request.text_search.IsNullOrEmpty() ? "" : request.text_search;
                request.user_id = null;
                long total = 0;
                var data = await Task.FromResult(_messageBUS.SearchListUser(request, out total));
                response.TotalItems = total;
                response.Data = data;
                response.Page = request.page;
                response.PageSize = request.pageSize;

            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("search-list-parent")]
        [HttpPost] //Tìm kiếm người dùng trong hệ thống
        public async Task<ResponseListMessage<List<UserInChatModel>>> SearchListParent(UserInChatRequest request)
        {
            var response = new ResponseListMessage<List<UserInChatModel>>();
            try
            {
                request.text_search = request.text_search.IsNullOrEmpty() ? "" : request.text_search;
                request.user_id = null;
                long total = 0;
                var data = await Task.FromResult(_messageBUS.SearchListUser(request, out total));
                response.TotalItems = total;
                response.Data = data;
                response.Page = request.page;
                response.PageSize = request.pageSize;

            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
    }
}
