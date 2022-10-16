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

namespace Digitizing.Api.Controllers
{
    [Route("api/tuition-fee")]
    [ApiController]
    public class TuitionFeeController : BaseController
    {
        private IWebHostEnvironment _env;
        private ITuitionFeeBusiness _tuitionFeeBUS;

        public TuitionFeeController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, ITuitionFeeBusiness tuitionFeeBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _tuitionFeeBUS = tuitionFeeBUS;
        }
        [Route("create-tuition-fee")]
        [HttpPost]
        public async Task<ResponseMessage<TuitionFeeModel>> CreateTuitionFee([FromBody] TuitionFeeModel model)
        {
            var response = new ResponseMessage<TuitionFeeModel>();
            try
            {
                if (model.tuition_fee_id == Guid.Empty) model.tuition_fee_id = Guid.NewGuid();
                model.created_by_user_id = CurrentUserId;
                var resultBUS = await Task.FromResult(_tuitionFeeBUS.Create(model));
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
        [Route("create-student-tuition-fee")]
        [HttpPost]
        public async Task<ResponseMessage<StudentTuitionFeeModel>> CreateStudentTuitionFee([FromBody] StudentTuitionFeeModel model)
        {
            var response = new ResponseMessage<StudentTuitionFeeModel>();
            try
            {
                model.created_by_user_id = CurrentUserId;
                var resultBUS = await Task.FromResult(_tuitionFeeBUS.CreateStudentTuitionFee(model));
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
        [Route("update-tuition-fee")]
        [HttpPost]
        public async Task<ResponseMessage<TuitionFeeModel>> UpdateTuitionFee([FromBody] TuitionFeeModel model)
        {
            var response = new ResponseMessage<TuitionFeeModel>();
            try
            {

                model.lu_user_id = CurrentUserId;
                var resultBUS = await Task.FromResult(_tuitionFeeBUS.Update(model));
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
        public async Task<ResponseListMessage<List<TuitionFeeSearchModel>>> Search([FromBody] TuitionFeeRequest request)
        {
            var response = new ResponseListMessage<List<TuitionFeeSearchModel>>();
            try
            {
                long total = 0;
                request.academy_year = request.academy_year.IsNullOrEmpty() ? "" : request.academy_year;
                request.semester = request.semester.IsNullOrEmpty() ? "" : request.semester;
                request.class_id = request.class_id.IsNullOrEmpty() ? "" : request.class_id;
                request.student_rcd = request.student_rcd.IsNullOrEmpty() ? "" : request.student_rcd;

                var data = await Task.FromResult(_tuitionFeeBUS.Search(request, out total));
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
        [Route("search-main")]
        [HttpPost]
        public async Task<ResponseListMessage<List<TuitionFeeModel>>> SearchTuitionFeeMain([FromBody] TuitionFeeMainRequest request)
        {
            var response = new ResponseListMessage<List<TuitionFeeModel>>();
            try
            {
                long total = 0;
                request.academy_year = request.academy_year.IsNullOrEmpty() ? "" : request.academy_year;
                request.semester = request.semester.IsNullOrEmpty() ? "" : request.semester;

                var data = await Task.FromResult(_tuitionFeeBUS.SearchTuitionFeeMain(request, out total));
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
        [Route("delete-tuition-fee")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteTuitionFee([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { tuition_fee_id = ds }).ToList());
                var listItem = await Task.FromResult(_tuitionFeeBUS.Delete(json_list_id, CurrentUserId));
                response.Data = listItem != null;
                response.MessageCode = MessageCodes.DeleteSuccessfully;
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
        [Route("delete-student-tuition-fee")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteStudentTuitionFee([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { student_tuition_fee_id = ds }).ToList());
                var listItem = await Task.FromResult(_tuitionFeeBUS.DeleteStudentTuitionFee(json_list_id, CurrentUserId));
                response.Data = listItem != null;
                response.MessageCode = MessageCodes.DeleteSuccessfully;
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }
    }
}
