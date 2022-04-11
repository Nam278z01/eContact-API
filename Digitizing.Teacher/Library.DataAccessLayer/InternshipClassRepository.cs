using System;
using System.Collections.Generic;
using Library.DataModel;
using Library.Common.Helper;
using System.Data;
using Library.DataAccessLayer.Interfaces;

namespace Library.DataAccessLayer
{
    public partial class InternshipClassRepository : IInternshipClassRepository
    {
        private IDatabaseHelper _dbHelper;
        public InternshipClassRepository(IDatabaseHelper dbHelper)
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
        public List<InternshipClassSearchModel> Search(int pageIndex, int pageSize, string class_id_rcd,
            string academic_year, int semester, string internship_id_rcd, out long total)
        {
            total = 0;
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@page_index", DbType.Int32, pageIndex),
                    _dbHelper.CreateInParameter("@page_size", DbType.Int32,  pageSize),
                    _dbHelper.CreateInParameter("@class_id_rcd", DbType.String,  class_id_rcd),
                    _dbHelper.CreateInParameter("@academic_year", DbType.String,  academic_year),
                    _dbHelper.CreateInParameter("@semester", DbType.Int32,  semester),
                    _dbHelper.CreateInParameter("@internship_id_rcd", DbType.String,  internship_id_rcd),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<InternshipClassSearchModel>("dbo.teacher_internship_class_search", parameters);
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
        public InternshipClassSearchModel GetById(string id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@internship_class_id",DbType.String, id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToFirstOrDefault<InternshipClassSearchModel>("dbo.leader_internship_class_get_by_id", parameters);
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

        /// <summary>
        /// Get information from the table WebsiteTag and push it into a list of type DropdownOptionModel
        /// </summary>
        /// <param name="lang">Language used to display data</param> 
        /// <returns></returns>
        public List<DropdownOptionModel> GetInternshipListDropdown()
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<DropdownOptionModel>("dbo.internship_get_list_dropdown", parameters);
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
    }
}
