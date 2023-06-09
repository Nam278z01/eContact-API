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
//using Library.BusinessLogicLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Digitizing.Api.Controllers
{
    [Route("api/internship-class")]
    [ApiController]
    public class InternshipClassController : BaseController
    {
        private IWebHostEnvironment _env;
        private IInternshipClassBusiness _internshipClassBUS;
        public InternshipClassController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IInternshipClassBusiness internshipClassBUS) : base((Library.Common.Caching.ICacheProvider)redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _internshipClassBUS = internshipClassBUS;
        }

        [Route("search")]
        [HttpPost]
        public async Task<ResponseListMessage<List<InternshipClassSearchModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<InternshipClassSearchModel>>();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var class_id_rcd = formData.Keys.Contains("class_id_rcd") ? Convert.ToString(formData["class_id_rcd"]) : "";

                var academic_year = formData.Keys.Contains("academic_year") ? Convert.ToString(formData["academic_year"]) : "";
                var semester = formData.Keys.Contains("semester") ? Convert.ToInt32(Convert.ToString(formData["semester"])) : 0;
                var internship_id_rcd = formData.Keys.Contains("internship_id_rcd") ? Convert.ToString(formData["internship_id_rcd"]) : "";
                long total = 0;
                var data = await Task.FromResult(_internshipClassBUS.Search(page, pageSize, class_id_rcd,
                     academic_year, semester, internship_id_rcd, out total));
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
        public async Task<ResponseMessage<InternshipClassSearchModel>> GetById(string id)
        {
            var response = new ResponseMessage<InternshipClassSearchModel>();
            try
            {
                response.Data = await Task.FromResult(_internshipClassBUS.GetById(id));
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
                response.Data = await Task.FromResult(_internshipClassBUS.GetClassListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("get-internship-dropdown")]
        [HttpGet]
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetInternshipListDropdown()
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_internshipClassBUS.GetInternshipListDropdown());
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

    }
}
