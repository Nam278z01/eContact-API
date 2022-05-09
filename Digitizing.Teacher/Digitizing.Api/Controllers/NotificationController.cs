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
//using Library.BusinessLogicLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Digitizing.Api.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private IWebHostEnvironment _env;
        private INotificationBusiness _internshipClassBUS;
        public NotificationController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, INotificationBusiness NotificationBUS) : base((Library.Common.Caching.ICacheProvider)redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _internshipClassBUS = NotificationBUS;
        }

        [Route("search")]
        [HttpPost]
        public async Task<ResponseListMessage<List<NotificationModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<NotificationModel>>();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var class_id = formData.Keys.Contains("class_id_rcd") ? Convert.ToString(formData["notification_type_id"]) : "";
                var student_name = formData.Keys.Contains("student_name") ? Convert.ToString(formData["notification_name"]) : "";
                //var course_year = formData.Keys.Contains("course_year") ? Convert.ToString(formData["course_year"]): "";
                long total = 0;
                var data = await Task.FromResult(_internshipClassBUS.Search(page, pageSize, class_id, student_name, out total));
                response.TotalItems = total;
                response.Data = data;
                response.Page = page;
                response.PageSize = pageSize;

            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        //[Route("get-by-id/{id}")]
        //[HttpGet]
        //public async Task<ResponseMessage<StudentRefModel>> GetById(string id)
        //{
        //    var response = new ResponseMessage<StudentRefModel>();
        //    try
        //    {
        //        response.Data = await Task.FromResult(_internshipClassBUS.GetById(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        response.MessageCode = ex.Message;
        //    }
        //    return response;
        //}

        //[Route("get-class-dropdown")]
        //[HttpGet]
        //public async Task<ResponseMessage<List<DropdownOptionModel>>> GetClassListDropdown()
        //{
        //    var response = new ResponseListMessage<List<DropdownOptionModel>>();
        //    try
        //    {
        //        response.Data = await Task.FromResult(_internshipClassBUS.GetClassListDropdown());
        //    }
        //    catch (Exception ex)
        //    {
        //        response.MessageCode = ex.Message;
        //    }
        //    return response;
        //}

        //[Route("update")]
        //[HttpPost]
        //public async Task<ResponseMessage<NotificationModel>> UpdateEvaluateRecruitment([FromBody] NotificationModel model)
        //{
        //    var response = new ResponseMessage<NotificationModel>();
        //    try
        //    {

        //        var resultBUS = await Task.FromResult(_internshipClassBUS.Update(model));
        //        if (resultBUS)
        //        {
        //            response.Data = model;
        //            response.MessageCode = MessageCodes.UpdateSuccessfully;
        //        }
        //        else
        //        {
        //            response.MessageCode = MessageCodes.UpdateFail;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.MessageCode = ex.Message;
        //    }
        //    return response;
        //}



        //[Route("get-internship-dropdown")]
        //[HttpGet]
        //public async Task<ResponseMessage<List<DropdownOptionModel>>> GetInternshipListDropdown()
        //{
        //    var response = new ResponseListMessage<List<DropdownOptionModel>>();
        //    try
        //    {
        //        response.Data = await Task.FromResult(_internshipClassBUS.GetInternshipListDropdown());
        //    }
        //    catch (Exception ex)
        //    {
        //        response.MessageCode = ex.Message;
        //    }
        //    return response;
        //}

    }
}
