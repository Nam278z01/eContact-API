﻿using System;
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
using DocumentFormat.OpenXml.EMMA;

namespace Digitizing.Api.Cms.Controllers
{
    [Route("api/tuition-fee")]
    [ApiController]
    public class TuitionFeeController : BaseController
    {
        private IWebHostEnvironment _env;
        private ITuitionFeeBusiness _BUS;
        private INotification2Business _BUS2;
        public TuitionFeeController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, ITuitionFeeBusiness BUS, INotification2Business BUS2) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _BUS = BUS;
            _BUS2 = BUS2;
        }

        [Route("Upload")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<ResponseMessage<List<string>>> Upload(string user_id, string class_id, string academy_year, int semester, IFormFile file)
        {
            var response = new ResponseMessage<List<string>> ();
            List<string> students = new List<string> ();
            try
            {
                if (file.Length > 0 && file.FileName.Contains(".xlsx"))
                {
                    var filename = Guid.NewGuid().ToString().Replace("-", "") + ".xlsx";
                    var webRoot = _env.ContentRootPath;
                    var filePath = Path.Combine(webRoot + "/Upload/", filename);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    var messageError = "";
                    var data = ImportExcel.ReadFromExcelFileForTuitionFee(filePath, out messageError);
                    var list = Tools.ConvertDataTable<TuitionFeeModel>(data);
                    foreach (var model in list)
                        if (!string.IsNullOrEmpty(model.tuition_fee_id.ToString()))
                        {
                            try
                            {
                                model.tuition_fee_id = Guid.NewGuid();
                                model.academy_year = academy_year;
                                model.semester = semester;
                                model.created_by_user_id = Guid.Parse(user_id);
                                string student_rcd = await Task.FromResult(_BUS.Create(model));
                                if(!string.IsNullOrEmpty(student_rcd))
                                {
                                    students.Add(student_rcd);
                                }
                            }
                            catch { }
                        }

                    if(students.Count > 0)
                    {
                        NotificationInfoModel notifyInfo = new NotificationInfoModel
                        {
                            notification_info_id = Guid.NewGuid(),
                            notification_title = "Thông báo học phí",
                            notification_content = "Học phí đã được cập nhập",
                            notification_type = 0,
                            class_id = class_id,
                            created_by_user_id = Guid.Parse(user_id),
                            receivers = students
                        };
                        await Task.FromResult(_BUS2.Create(notifyInfo));
                    }

                    response.Data = students;
                    response.MessageCode = MessageCodes.UpdateSuccessfully;
                }
                else
                {
                    response.MessageCode = MessageCodes.UpdateFail;
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
        public async Task<ResponseListMessage<List<TuitionFeeSearchModel>>> Search([FromBody] TuitionFeeRequestModel request)
        {
            var response = new ResponseListMessage<List<TuitionFeeSearchModel>>();
            try
            {

                long total = 0;
                var data = await Task.FromResult(_BUS.Search(request, out total));
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
