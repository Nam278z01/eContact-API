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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Digitizing.Api.Controllers
{
    [Route("api/report-recruitment")]
    [ApiController]
    public class ReportRecuitmentController : BaseController
    {
        private IWebHostEnvironment _env;
        private IReportRecruitmentBusiness _reportRecruitmentClassBUS;
        public ReportRecuitmentController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IReportRecruitmentBusiness reportRecruitmentBUS) : base((Library.Common.Caching.ICacheProvider)redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _reportRecruitmentClassBUS = reportRecruitmentBUS;
        }

        [Route("search")]
        [HttpPost]
        public async Task<ResponseListMessage<List<RecruitmentReportSearchModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<RecruitmentReportSearchModel>>();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var user_id = formData.Keys.Contains("user_id") ? Convert.ToString(formData["user_id"]) : "";
                var class_id = formData.Keys.Contains("class_id") ? Convert.ToString(formData["class_id"]) : "";
                var student_rcd = formData.Keys.Contains("student_rcd") ? Convert.ToString(formData["student_rcd"]) : "";
                var student_name = formData.Keys.Contains("student_name") ? Convert.ToString(formData["student_name"]) : "";
                var report_week = formData.Keys.Contains("report_week") ? Convert.ToInt32(Convert.ToString(formData["report_week"])) : 1;
                var academic_year = formData.Keys.Contains("academic_year") ? Convert.ToString(formData["academic_year"]) : "";
                var company_rcd = formData.Keys.Contains("company_rcd") ? Convert.ToString(formData["company_rcd"]) : "";
                long total = 0;
                var data = await Task.FromResult(_reportRecruitmentClassBUS.Search(page, pageSize, user_id, class_id,
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

        [Route("search-report")]
        [HttpPost]
        public async Task<ResponseMessage<List<RecruitmentReportModel>>> SearchReport([FromBody] Dictionary<string, object> formData)
        {

            var response = new ResponseListMessage<List<RecruitmentReportModel>>();
            try
            {
                var page = 1;
                var pageSize = 10;
                var student_rcd = formData.Keys.Contains("student_rcd") ? Convert.ToString(formData["student_rcd"]) : "";
           
                var report_week_rcd = formData.Keys.Contains("report_week") ? Convert.ToInt32(Convert.ToString(formData["report_week"])) : 1;
                long total = 0;
                var data = await Task.FromResult(_reportRecruitmentClassBUS.GetReportDetail(page, pageSize, student_rcd,
                     report_week_rcd, out total));
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

        [Route("get-class-dropdown")]
        [HttpGet]
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetClassListDropdown()
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_reportRecruitmentClassBUS.GetClassListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("get-company-dropdown")]
        [HttpGet]
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetCompanyListDropdown()
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_reportRecruitmentClassBUS.GetCompanyListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

    }
}
