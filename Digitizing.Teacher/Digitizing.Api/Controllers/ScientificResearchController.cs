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
    [Route("api/scientific-research")]
    [ApiController]
    public class ScientificResearchController : BaseController
    {
        private IWebHostEnvironment _env;
        private IScientificResearchBusiness _scientificResearchBUS;
        public ScientificResearchController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IScientificResearchBusiness scientificResearchBUS) : base((Library.Common.Caching.ICacheProvider)redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _scientificResearchBUS = scientificResearchBUS;
        }
        [Route("create")]
        [HttpPost]
        public async Task<ResponseMessage<ScientificResearchModel>> create([FromBody] ScientificResearchModel model)
        {
            var response = new ResponseMessage<ScientificResearchModel>();
            try
            {
                model.created_by_user_id = CurrentUserId;
                var resultBUS = await Task.FromResult(_scientificResearchBUS.Create(model));
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

        [Route("update")]
        [HttpPost]
        public async Task<ResponseMessage<ScientificResearchModel>> update([FromBody] ScientificResearchModel model)
        {
            var response = new ResponseMessage<ScientificResearchModel>();
            try
            {

                var resultBUS = await Task.FromResult(_scientificResearchBUS.Update(model));
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
        public async Task<ResponseListMessage<List<ScientificResearchSearchModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<ScientificResearchSearchModel>>();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var category_type_id = formData.Keys.Contains("category_type_id") ? Convert.ToString(formData["category_type_id"]) : "";
                var student_id = formData.Keys.Contains("student_id") ? Convert.ToString(formData["student_id"]) : "";
                var teacher_id = formData.Keys.Contains("teacher_id") ? Convert.ToString(formData["teacher_id"]) : "";
                var research_project_name = formData.Keys.Contains("research_project_name") ? Convert.ToString(formData["research_project_name"]) : "";
                var content = formData.Keys.Contains("content") ? Convert.ToString(formData["content"]) : "";
                var rate_id = formData.Keys.Contains("rate_id") ? Convert.ToString(formData["rate_id"]) : "";
                var rank = formData.Keys.Contains("rank") ? Convert.ToString(formData["rank"]) : "";
                long total = 0;
                var data = await Task.FromResult(_scientificResearchBUS.Search(page, pageSize, category_type_id, 
                    student_id, teacher_id, research_project_name, content, rate_id, rank, out total));
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

        [Route("get-by-id/{id}")]
        [HttpGet]
        public async Task<ResponseMessage<ScientificResearchModel>> GetById(string id)
        {
            var response = new ResponseMessage<ScientificResearchModel>();
            try
            {
                response.Data = await Task.FromResult(_scientificResearchBUS.GetById(id));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("get-category-dropdown")]
        [HttpGet]
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetCategoryListDropdown()
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_scientificResearchBUS.GetCategoryListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("get-student-dropdown")]
        [HttpGet]
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetStudentListDropdown()
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_scientificResearchBUS.GetStudentListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("get-teacher-dropdown")]
        [HttpGet]
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetTeacherListDropdown()
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_scientificResearchBUS.GetTeacherListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("get-teacher-rate-dropdown")]
        [HttpGet]
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetTeacherRateListDropdown()
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_scientificResearchBUS.GetTeacherRateListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
    }
}
