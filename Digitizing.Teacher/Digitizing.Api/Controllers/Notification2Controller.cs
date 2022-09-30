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
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.DependencyInjection;

namespace Digitizing.Api.Controllers
{
    [Route("api/notification2")]
    [ApiController]
    public class Notification2Controller : BaseController
    {
        private IWebHostEnvironment _env;
        private INotification2Business _notification2BUS;
        public Notification2Controller(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, INotification2Business notification2BUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _notification2BUS = notification2BUS;
        }

        [Route("create-notification2")]
        [HttpPost]
        public async Task<ResponseMessage<NotificationInfoModel>> CreateNotification2([FromBody] NotificationInfoModel model)
        {
            var response = new ResponseMessage<NotificationInfoModel>();
            try
            {
                model.created_by_user_id = CurrentUserId;
                var resultBUS = await Task.FromResult(_notification2BUS.Create(model));
                if (resultBUS)
                {
                    response.Data = model;
                    response.MessageCode = MessageCodes.CreateSuccessfully;
                }
                else
                {
                    response.MessageCode = MessageCodes.CreateFail;
                }

            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("search")]
        [HttpPost]
        public async Task<ResponseListMessage<List<NotificationInfoSearchModel>>> Search([FromBody] NotificationInfoRequest request)
        {
            var response = new ResponseListMessage<List<NotificationInfoSearchModel>>();
            try
            {
                long total = 0;
                request.class_id = request.class_id.IsNullOrEmpty() ? "" : request.class_id;
                request.user_id = CurrentUserId;

                var data = await Task.FromResult(_notification2BUS.Search(request, out total));
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

        [Route("get-by-id/{id}")]
        [HttpGet]
        public async Task<ResponseMessage<NotificationInfoModel>> GetById(Guid? id)
        {
            var response = new ResponseMessage<NotificationInfoModel>();
            try
            {
                response.Data = await Task.FromResult(_notification2BUS.GetById(id));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("delete-notification2")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteNotification2([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { notification_info_id = ds }).ToList());
                var listItem = await Task.FromResult(_notification2BUS.Delete(json_list_id, CurrentUserId));
                response.Data = listItem != null;
                response.MessageCode = MessageCodes.DeleteSuccessfully;
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("get-parents-dropdown-by-class")]
        [HttpGet]
        public async Task<ResponseMessage<IList<DropdownOptionModel>>> GetParentsListDropdownByClass(string class_id)
        {
            class_id = class_id.IsNullOrEmpty() ? "" : class_id;
            var response = new ResponseMessage<IList<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_notification2BUS.GetParentsListDropdownByClass(class_id));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("get-my-notification2")]
        [HttpPost]
        public async Task<ResponseListMessage<List<Notification2SearchModel>>> GetMyNotification([FromBody] Notification2Request request)
        {
            var response = new ResponseListMessage<List<Notification2SearchModel>>();
            try
            {
                long total = 0;
                request.user_id = CurrentUserId;

                var data = await Task.FromResult(_notification2BUS.GetMyNotification(request, out total));
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
        [Route("get-my-notification2-by-id/{id}")]
        [HttpGet]
        public async Task<ResponseMessage<Notification2SearchModel>> GetMyNotificationById(Guid? id)
        {
            var response = new ResponseMessage<Notification2SearchModel>();
            try
            {
                response.Data = await Task.FromResult(_notification2BUS.GetMyNotificationById(id, CurrentUserId));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("check-notification2")]
        [HttpPost]
        public async Task<ResponseMessage<bool>> CheckNotification()
        {
            var response = new ResponseMessage<bool>();
            try
            {
                var resultBUS = await Task.FromResult(_notification2BUS.CheckNotification(CurrentUserId));
                if (resultBUS)
                {
                    response.Data = true;
                    response.MessageCode = MessageCodes.SendSuccess;
                }
                else
                {
                    response.MessageCode = MessageCodes.CreateFail;
                }

            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("count-unchecked-notification2")]
        [HttpPost]
        public async Task<ResponseMessage<int>> CountUnCheckedNotification()
        {
            var response = new ResponseMessage<int>();
            try
            {
                 response.Data = await Task.FromResult(_notification2BUS.CountUnCheckedNotification(CurrentUserId)); ;
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
    }
}
