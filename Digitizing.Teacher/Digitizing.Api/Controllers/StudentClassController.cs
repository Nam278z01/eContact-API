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
//using Library.BusinessLogicLayer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Digitizing.Api.Controllers
{
    [Route("api/student-class")]
    [ApiController]
    public class StudentClassController : BaseController
    {
        private IWebHostEnvironment _env;
        private IStudentClassBusiness _internshipClassBUS;
        public StudentClassController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IStudentClassBusiness StudentClassBUS) : base((Library.Common.Caching.ICacheProvider)redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _internshipClassBUS = StudentClassBUS;
        }

        [Route("search")]
        [HttpPost]
        public async Task<ResponseListMessage<List<StudentClassModel>>> Search([FromBody] Dictionary<string, object> formData)
        {
            var response = new ResponseListMessage<List<StudentClassModel>>();
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var user_id = formData.Keys.Contains("user_id") ? Convert.ToString(formData["user_id"]) : "";
                var class_id = formData.Keys.Contains("class_id_rcd") ? Convert.ToString(formData["class_id_rcd"]) : "";
                var student_name = formData.Keys.Contains("student_name") ? Convert.ToString(formData["student_name"]) : "";
                //var course_year = formData.Keys.Contains("course_year") ? Convert.ToString(formData["course_year"]): "";
                long total = 0;
                var data = await Task.FromResult(_internshipClassBUS.Search(page, pageSize, user_id, class_id, student_name, out total));
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
        public async Task<ResponseMessage<StudentRefModel>> GetById(string id)
        {
            var response = new ResponseMessage<StudentRefModel>();
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
        public async Task<ResponseMessage<List<DropdownOptionModel>>> GetClassListDropdown(string teacher_id_rcd)
        {
            var response = new ResponseListMessage<List<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_internshipClassBUS.GetClassListDropdown( teacher_id_rcd));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("update")]
        [HttpPost]
        public async Task<ResponseMessage<StudentClassModel>> UpdateEvaluateRecruitment([FromBody] StudentClassModel model)
        {
            var response = new ResponseMessage<StudentClassModel>();
            try
            {

                var resultBUS = await Task.FromResult(_internshipClassBUS.Update(model));
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
        [Route("get-student-dropdown-by-family")]
        [HttpGet]
        public async Task<ResponseMessage<IList<DropdownOptionModel>>> GetStudentListDropdownByFamily()
        {
            var response = new ResponseMessage<IList<DropdownOptionModel>>();
            try
            {
                response.Data = await Task.FromResult(_internshipClassBUS.GetStudentListDropdownByFamily(CurrentUserId));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
    }
}
