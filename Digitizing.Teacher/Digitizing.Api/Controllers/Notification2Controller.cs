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
    }
}
