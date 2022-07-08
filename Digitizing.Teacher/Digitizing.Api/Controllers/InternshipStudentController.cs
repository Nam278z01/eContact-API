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
    [Route("api/internship-student")]
    [ApiController]
    public class InternshipStudentController : BaseController
    {
        private IWebHostEnvironment _env;
        private IInternshipStudentBusiness _internshipClassBUS;
        private IReportRecruitmentBusiness _reportRecruitmentClassBUS;
        public InternshipStudentController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IInternshipStudentBusiness internshipStudentBUS, IReportRecruitmentBusiness reportRecruitmentBUS) : base((Library.Common.Caching.ICacheProvider)redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _internshipClassBUS = internshipStudentBUS;
            _reportRecruitmentClassBUS = reportRecruitmentBUS;
        }

        [Route("search")]
        [HttpPost]
        public async Task<ResponseListMessage<List<InternshipStudentSearchModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<InternshipStudentSearchModel>>();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var user_id = formData.Keys.Contains("user_id") ? Convert.ToString(formData["user_id"]) : "";
                var class_id_rcd = formData.Keys.Contains("class_id_rcd") ? Convert.ToString(formData["class_id_rcd"]) : "";
                var school_year = formData.Keys.Contains("school_year") ? Convert.ToString(formData["school_year"]) : "";
                var course_year = formData.Keys.Contains("course_year") ? Convert.ToString(formData["course_year"]): "";
                long total = 0;
                var data = await Task.FromResult(_internshipClassBUS.Search(page, pageSize, user_id, class_id_rcd,
                    school_year, course_year,
                    out total));
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
        //public async Task<ResponseMessage<InternshipClassSearchModel>> GetById(string id)
        //{
        //    var response = new ResponseMessage<InternshipClassSearchModel>();
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

        [Route("get-class-dropdown")]
        [HttpGet]
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetClassListDropdown()
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_internshipClassBUS.GetClassListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("get-course-year-list-drop-down")]
        [HttpGet]
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetCourseYearListDropdown()
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_internshipClassBUS.GetCourseYearListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("get-school-year-list-drop-down")]
        [HttpGet]
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetSchoolYearListDropdown()
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_internshipClassBUS.GetSchoolYearListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("get-company-list-drop-down")]
        [HttpGet]
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetCompanyListDropdown()
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_internshipClassBUS.GetCompanyListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("search-Report")]
        [HttpPost]
        public async Task<ResponseListMessage<List<RecruitmentReportSearchModel>>> SearchReport([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<RecruitmentReportSearchModel>>();
            try
            {
                var page = 1;
                var pageSize = 10;
                var class_id = formData.Keys.Contains("class_id_rcd") ? Convert.ToString(formData["class_id_rcd"]) : "";
                var student_rcd = formData.Keys.Contains("student_rcd") ? Convert.ToString(formData["student_rcd"]) : "";
                var student_name = formData.Keys.Contains("student_name") ? Convert.ToString(formData["student_name"]) : "";
                var report_week = formData.Keys.Contains("report_week") ? Convert.ToInt32(Convert.ToString(formData["report_week"])) : 0;
                var academic_year = formData.Keys.Contains("course_year") ? Convert.ToString(formData["course_year"]) : "";
                var company_rcd = formData.Keys.Contains("company_rcd") ? Convert.ToString(formData["company_rcd"]) : "";
                long total = 0;
                var data = await Task.FromResult(_reportRecruitmentClassBUS.Search(page, pageSize, class_id,
                     student_rcd, student_name, academic_year, report_week, company_rcd, out total));
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
    }
}
