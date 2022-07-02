using System;
using System.Collections.Generic;
using Library.DataModel;
using Library.Common.Helper;
using System.Data;
using Library.DataAccessLayer;

namespace Library.DataAccessLayer
{
    public partial class InternshipStudentRepository : IInternshipStudentRepository
    {
        private IDatabaseHelper _dbHelper;
        public InternshipStudentRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        /// <summary>
        /// Searching information in the table WebsiteTag
        /// </summary>
        /// <param name="pageIndex">which page?</param>
        /// <param name="pageSize">the number of records in a page</param>
        /// <param name="lang"> Language used to display data</param>
        /// <param name="total">the total number of records</param> 
        /// <returns></returns>
        public List<InternshipStudentSearchModel> Search(int pageIndex, int pageSize,string user_id, string class_id_rcd,
            string school_year, string course_year,
             out long total)
        {
            total = 0;
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@page_index", DbType.Int32, pageIndex),
                    _dbHelper.CreateInParameter("@page_size", DbType.Int32,  pageSize),
                    _dbHelper.CreateInParameter("@user_id", DbType.String,  user_id),
                    _dbHelper.CreateInParameter("@class_id_rcd", DbType.String,  class_id_rcd),
                    _dbHelper.CreateInParameter("@school_year", DbType.String,  school_year),
                    _dbHelper.CreateInParameter("@course_year", DbType.String,  course_year),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<InternshipStudentSearchModel>("dbo.teacher_internship_student_search", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
                    throw new Exception(result.ErrorMessage);

                if (result.Output["OUT_TOTAL_ROW"] + "" != "")
                    total = Convert.ToInt32(result.Output["OUT_TOTAL_ROW"]);
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get the information by using id of the table WebsiteTag
        /// </summary>
        /// <param name="id">Id used to get the information</param>
        /// <returns></returns>
        //public InternshipStudentSearchModel GetById(string id)
        //{
        //    try
        //    {
        //        var parameters = new List<IDbDataParameter>
        //        {
        //            _dbHelper.CreateInParameter("@internship_class_id",DbType.String, id),
        //            _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
        //            _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
        //        };
        //        var result = _dbHelper.CallToFirstOrDefault<InternshipStudentSearchModel>("dbo.leader_internship_class_get_by_id", parameters);
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
        /// Get information from the table WebsiteTag and push it into a list of type DropdownOptionModel
        /// </summary>
        /// <param name="lang">Language used to display data</param> 
        /// <returns></returns>
        public List<DropdownOptionModel> GetClassListDropdown()
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<DropdownOptionModel>("dbo.internship_class_get_list_dropdown", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
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

        public List<DropdownOptionModel> GetCourseYearListDropdown()
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@prev", DbType.Int32,5),
                    _dbHelper.CreateInParameter("@next", DbType.Int32, 1),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<DropdownOptionModel>("dbo.course_year_get_list_dropdown", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
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

        public List<DropdownOptionModel> GetSchoolYearListDropdown()
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<DropdownOptionModel>("dbo.school_year_get_list_dropdown", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
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
        /// Get information from the table WebsiteTag and push it into a list of type DropdownOptionModel
        /// </summary>
        /// <param name="lang">Language used to display data</param> 
        /// <returns></returns>
        //public List<DropdownOptionModel> GetInternshipListDropdown()
        //{
        //    try
        //    {
        //        var parameters = new List<IDbDataParameter>
        //        {
        //            _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
        //            _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
        //        };
        //        var result = _dbHelper.CallToList<DropdownOptionModel>("dbo.internship_get_list_dropdown", parameters);
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
