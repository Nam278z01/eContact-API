using System;
using System.Collections.Generic;
using Library.DataModel;
using Library.Common.Helper;
using System.Data;
using System.Linq;

namespace Library.DataAccessLayer
{
    public partial class EvaluateRecruitmentRepository : IEvaluateRecruitmentRepository
    {
        private IDatabaseHelper _dbHelper;
        public EvaluateRecruitmentRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        /// <summary>
        /// Add a new records into the table EvaluateRecruitment 
        /// </summary>
        /// <param name="model">the record added </param>
        /// <returns></returns>
        public bool Create(EvaluateRecruitmentModel model)
        {
            try
            {
                 model.evaluate_recruitment_rcd = Guid.NewGuid();
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@evaluate_recruitment_rcd",DbType.Guid,model.evaluate_recruitment_rcd),
                    _dbHelper.CreateInParameter("@student_rcd",DbType.String,model.student_rcd),
                    _dbHelper.CreateInParameter("@point",DbType.Double,model.point),
                    _dbHelper.CreateInParameter("@academic_year",DbType.String,model.academic_year),
                    _dbHelper.CreateInParameter("@class_id",DbType.String,model.class_id),
                    _dbHelper.CreateInParameter("@created_by_user_id",DbType.Guid,model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.teacher_evaluate_recruitment_create", parameters);
                if ((result != null && !string.IsNullOrEmpty(result.ErrorMessage)) && result.ErrorCode != 0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                else if (result.Value != null && result.Value.ToString().IndexOf("MESSAGE") >= 0)
                {
                    throw new Exception(result.Value.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Update information in the tableEvaluateRecruitment
        /// </summary>
        /// <param name="model">the record updated</param>
        /// <returns></returns>
        public bool Update(EvaluateRecruitmentModel model)
        {
            try
            {

                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@evaluate_recruitment_rcd",DbType.String,model.evaluate_recruitment_rcd),
                    _dbHelper.CreateInParameter("@student_rcd",DbType.String,model.student_rcd),
                    _dbHelper.CreateInParameter("@point",DbType.Double,model.point),
                    _dbHelper.CreateInParameter("@academic_year",DbType.String,model.academic_year),
                    _dbHelper.CreateInParameter("@class_id",DbType.String,model.class_id),
                    _dbHelper.CreateInParameter("@created_by_user_id",DbType.Guid,model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.teacher_evaluate_recruitment_update", parameters);
                if ((result != null && !string.IsNullOrEmpty(result.ErrorMessage)) && result.ErrorCode != 0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                else if (result.Value != null && result.Value.ToString().IndexOf("MESSAGE") >= 0)
                {
                    throw new Exception(result.Value.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Searching information in the table EvaluateRecruitment
        /// </summary>
        /// <param name="pageIndex">which page?</param>
        /// <param name="pageSize">the number of records in a page</param>
        /// <param name="lang"> Language used to display data</param>
        /// <param name="total">the total number of records</param> 
        /// <returns></returns>
        //public List<EvaluateRecruitmentModel> Search(int pageIndex, int pageSize, char lang, out long total, string tag_name)
        //{
        //    total = 0;
        //    try
        //    {
        //        var parameters = new List<IDbDataParameter>
        //        {
        //            _dbHelper.CreateInParameter("@page_index", DbType.Int32, pageIndex),
        //            _dbHelper.CreateInParameter("@page_size", DbType.Int32,  pageSize),
        //            _dbHelper.CreateInParameter("@lang", DbType.String,  lang),
        //            _dbHelper.CreateInParameter("@tag_name" ,DbType.String,tag_name),
        //            _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
        //            _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
        //            _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
        //        };
        //        var result = _dbHelper.CallToList<EvaluateRecruitmentModel>("dbo.website_tag_search", parameters);
        //        if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
        //            throw new Exception(result.ErrorMessage);

        //        if (result.Output["OUT_TOTAL_ROW"] + "" != "")
        //            total = Convert.ToInt32(result.Output["OUT_TOTAL_ROW"]);
        //        return result.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        /// <summary>
        /// Get the information by using id of the table EvaluateRecruitment
        /// </summary>
        /// <param name="id">Id used to get the information</param>
        /// <returns></returns>
        public EvaluateRecruitmentModel GetById(Guid? id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@evaluate_recruitment_rcd",DbType.Guid, id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToFirstOrDefault<EvaluateRecruitmentModel>("dbo.teacher_evaluate_recruitment_get_by_id", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) || result.ErrorCode != 0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Delete records in the table EvaluateRecruitment 
        /// </summary>
        /// <param name="json_list_id">List id want to delete</param>
        /// <param name="updated_by">User made the deletion</param>
        /// <returns></returns>
        //public List<EvaluateRecruitmentModel> Delete(string json_list_id, Guid updated_by)
        //{
        //    try
        //    {
        //        var parameters = new List<IDbDataParameter>
        //        {
        //            _dbHelper.CreateInParameter("@json_list_id", DbType.String, json_list_id),
        //            _dbHelper.CreateInParameter("@updated_by", DbType.Guid, updated_by),
        //            _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
        //            _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
        //        };
        //        var result = _dbHelper.CallToList<EvaluateRecruitmentModel>("dbo.website_tag_delete_multi", parameters);
        //        if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
        //        {
        //            throw new Exception(result.ErrorMessage);
        //        }
        //        return result.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// <summary>
        /// Get information from the table EvaluateRecruitment and push it into a list of type DropdownOptionModel
        /// </summary>
        /// <param name="lang">Language used to display data</param> 
        /// <returns></returns>
        //public List<DropdownOptionModel> GetListDropdown(char lang)
        //{
        //    try
        //    {
        //        var parameters = new List<IDbDataParameter>
        //        {
        //            _dbHelper.CreateInParameter("@lang", DbType.String, lang),
        //            _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
        //            _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
        //        };
        //        var result = _dbHelper.CallToList<DropdownOptionModel>("dbo.website_tag_get_list_dropdown", parameters);
        //        if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
        //        {
        //            throw new Exception(result.ErrorMessage);
        //        }
        //        return result.Value;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}