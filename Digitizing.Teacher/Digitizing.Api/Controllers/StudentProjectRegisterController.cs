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
    [Route("api/student-project-register")]
    [ApiController]
    public class StudentProjectRegisterController : BaseController
    {
        private IWebHostEnvironment _env;
        private IStudentProjectRegisterBusiness _studentProjectRegisterBUS;
        public StudentProjectRegisterController(ICacheProvider redis, IConfiguration configuration, 
            IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IStudentProjectRegisterBusiness studentProjectRegisterBUS) : base((Library.Common.Caching.ICacheProvider)redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _studentProjectRegisterBUS = studentProjectRegisterBUS;
        }
        //[Route("create")]
        //[HttpPost]
        //public async Task<ResponseMessage<ScientificResearchModel>> create([FromBody] ScientificResearchModel model)
        //{
        //    var response = new ResponseMessage<ScientificResearchModel>();
        //    try
        //    {
        //        model.created_by_user_id = CurrentUserId;
        //        var resultBUS = await Task.FromResult(_studentProjectRegisterBUS.Create(model));
        //        if (resultBUS)
        //        {
        //            response.Data = model;
        //            response.MessageCode = MessageCodes.CreateSuccessfully;
        //        }
        //        else
        //        {
        //            response.MessageCode = MessageCodes.CreateFail;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        response.MessageCode = ex.Message;
        //    }
        //    return response;
        //}

        [Route("update")]
        [HttpPost]
        public async Task<ResponseMessage<StudentProjectRegisterModel>> update([FromBody] StudentProjectRegisterModel model)
        {
            var response = new ResponseMessage<StudentProjectRegisterModel>();
            try
            {

                var resultBUS = await Task.FromResult(_studentProjectRegisterBUS.Update(model));
                if (resultBUS)
                {
                    response.Data = model;
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
        public async Task<ResponseListMessage<List<StudentProjectRegisterSearchModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<StudentProjectRegisterSearchModel>>();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var student_project_name = formData.Keys.Contains("student_project_name") ? Convert.ToString(formData["student_project_name"]) : "";
                var academic_year = formData.Keys.Contains("academic_year") ? Convert.ToString(formData["academic_year"]) : "";
                var semester = formData.Keys.Contains("semester") ? int.Parse(Convert.ToString(formData["semester"])) : 1;
                var class_id = formData.Keys.Contains("class_id") ? Convert.ToString(formData["class_id"]) : "";
                long total = 0;
                var data = await Task.FromResult(_studentProjectRegisterBUS.Search(page, pageSize, student_project_name, 
                    academic_year, semester, class_id, out total));
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
        //public async Task<ResponseMessage<ScientificResearchModel>> GetById(string id)
        //{
        //    var response = new ResponseMessage<ScientificResearchModel>();
        //    try
        //    {
        //        response.Data = await Task.FromResult(_studentProjectRegisterBUS.GetById(id));
        //    }
        //    catch (Exception ex)
        //    {
        //        response.MessageCode = ex.Message;
        //    }
        //    return response;
        //}

        //[Route("delete")]
        //[HttpPost]
        //public async Task<ResponseListMessage<bool>> Delete([FromBody] List<string> items)
        //{
        //    var response = new ResponseListMessage<bool>();
        //    try
        //    {
        //        var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { scientific_research_id = ds }).ToList());
        //        var listItem = await Task.FromResult(_studentProjectRegisterBUS.Delete(json_list_id, CurrentUserId));
        //        response.Data = listItem != null;
        //        response.MessageCode = MessageCodes.DeleteSuccessfully;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.MessageCode = ex.Message;
        //    }
        //    return response;
        //}


        //[Route("get-category-dropdown")]
        //[HttpGet]
        //public async Task<ResponseMessage<List<DropdownOptionModel>>> GetCategoryListDropdown()
        //{
        //    var response = new ResponseListMessage<List<DropdownOptionModel>>();
        //    try
        //    {
        //        response.Data = await Task.FromResult(_studentProjectRegisterBUS.GetCategoryListDropdown());
        //    }
        //    catch (Exception ex)
        //    {
        //        response.MessageCode = ex.Message;
        //    }
        //    return response;
        //}

        //[Route("get-student-dropdown")]
        //[HttpGet]
        //public async Task<ResponseMessage<List<DropdownOptionModel>>> GetStudentListDropdown()
        //{
        //    var response = new ResponseListMessage<List<DropdownOptionModel>>();
        //    try
        //    {
        //        response.Data = await Task.FromResult(_studentProjectRegisterBUS.GetStudentListDropdown());
        //    }
        //    catch (Exception ex)
        //    {
        //        response.MessageCode = ex.Message;
        //    }
        //    return response;
        //}

        //[Route("get-teacher-dropdown")]
        //[HttpGet]
        //public async Task<ResponseMessage<List<DropdownOptionModel>>> GetTeacherListDropdown()
        //{
        //    var response = new ResponseListMessage<List<DropdownOptionModel>>();
        //    try
        //    {
        //        response.Data = await Task.FromResult(_studentProjectRegisterBUS.GetTeacherListDropdown());
        //    }
        //    catch (Exception ex)
        //    {
        //        response.MessageCode = ex.Message;
        //    }
        //    return response;
        //}

        //[Route("get-teacher-rate-dropdown")]
        //[HttpGet]
        //public async Task<ResponseMessage<List<DropdownOptionModel>>> GetTeacherRateListDropdown()
        //{
        //    var response = new ResponseListMessage<List<DropdownOptionModel>>();
        //    try
        //    {
        //        response.Data = await Task.FromResult(_studentProjectRegisterBUS.GetTeacherRateListDropdown());
        //    }
        //    catch (Exception ex)
        //    {
        //        response.MessageCode = ex.Message;
        //    }
        //    return response;
        //}
    }
}
