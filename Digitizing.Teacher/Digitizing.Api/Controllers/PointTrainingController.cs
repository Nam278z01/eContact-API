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
    [Route("api/point-training")]
    [ApiController]
    public class PointTrainingController : BaseController
    {
        private IWebHostEnvironment _env;
        private IPointTrainingBusiness _pointTrainingBUS;
        public PointTrainingController(ICacheProvider redis, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env, IPointTrainingBusiness pointTrainingBUS) : base(redis, configuration, httpContextAccessor)
        {
            _env = env ?? throw new ArgumentNullException(nameof(env));
            _pointTrainingBUS = pointTrainingBUS;
        }

        [Route("create-point-training")]
        [HttpPost]
        public async Task<ResponseMessage<PointTrainingModel>> CreatePointTraining([FromBody] PointTrainingModel model)
        {
            var response = new ResponseMessage<PointTrainingModel>();
            try
            {
                model.created_by_user_id = CurrentUserId;
                var resultBUS = await Task.FromResult(_pointTrainingBUS.Create(model));
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

        [Route("update-point-training")]
        [HttpPost]
        public async Task<ResponseMessage<PointTrainingModel>> UpdatePointTraining([FromBody] PointTrainingModel model)
        {
            var response = new ResponseMessage<PointTrainingModel>();
            try
            {

                model.lu_user_id = CurrentUserId;
                var resultBUS = await Task.FromResult(_pointTrainingBUS.Update(model));
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
        public async Task<ResponseListMessage<List<PointTrainingSearchModel>>> Search([FromBody] PointTrainingRequest request)
        {
            var response = new ResponseListMessage<List<PointTrainingSearchModel>>();
            try
            {
                long total = 0;
                request.academy_year = request.academy_year.IsNullOrEmpty() ? "" : request.academy_year;
                request.semester = request.semester.IsNullOrEmpty() ? "" : request.semester;
                request.class_id = request.class_id.IsNullOrEmpty() ? "" : request.class_id;
                request.student_rcd = request.student_rcd.IsNullOrEmpty() ? "" : request.student_rcd;
                request.student_name = request.student_name.IsNullOrEmpty() ? "" : request.student_name;

                var data = await Task.FromResult(_pointTrainingBUS.Search(request, out total));
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

        [Route("get-by-id/{id}")]
        [HttpGet]
        public async Task<ResponseMessage<PointTrainingModel>> GetById(Guid? id)
        {
            var response = new ResponseMessage<PointTrainingModel>();
            try
            {
                response.Data = await Task.FromResult(_pointTrainingBUS.GetById(id));
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("delete-point-training")]
        [HttpPost]
        public async Task<ResponseListMessage<bool>> DeleteWebsiteTag([FromBody] List<string> items)
        {
            var response = new ResponseListMessage<bool>();
            try
            {
                var json_list_id = MessageConvert.SerializeObject(items.Select(ds => new { point_training_id = ds }).ToList());
                var listItem = await Task.FromResult(_pointTrainingBUS.Delete(json_list_id, CurrentUserId));
                response.Data = listItem != null;
                response.MessageCode = MessageCodes.DeleteSuccessfully;
            }
            catch (Exception ex)
            {
                response.MessageCode = ex.Message;
            }
            return response;
        }

        [Route("Upload")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(string academy_year, int semester, IFormFile file)
        {
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
                    var data = ImportExcel.ReadFromExcelFileForPointTraining(filePath, 1, out messageError);
                    var list = Tools.ConvertDataTable<PointTrainingModel>(data);
                    foreach (var model in list)
                        if (!string.IsNullOrEmpty(model.student_rcd))
                        {
                            try
                            {
                                model.point_training_id = Guid.NewGuid();
                                model.academy_year = academy_year;
                                model.semester = semester;
                                model.created_by_user_id = CurrentUserId;
                                await Task.FromResult(_pointTrainingBUS.Create(model));
                            }
                            catch { }
                            {
                                try
                                {
                                    model.point_training_id = Guid.NewGuid();
                                    model.academy_year = academy_year;
                                    model.semester = semester;
                                    model.lu_user_id = CurrentUserId;
                                    await Task.FromResult(_pointTrainingBUS.Update(model));
                                }
                                catch { }
                            }
                        }
                    return Ok(new { MessageCodes.UpdateSuccessfully });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return Ok(new { MessageCodes.UpdateFail });
            }
        }
    }
}
