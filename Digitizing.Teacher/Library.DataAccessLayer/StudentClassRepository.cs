using System;
using System.Collections.Generic;
using Library.DataModel;
using Library.Common.Helper;
using System.Data;
using Library.DataAccessLayer;

namespace Library.DataAccessLayer
{
    public partial class StudentClassRepository : IStudentClassRepository
    {
        private IDatabaseHelper _dbHelper;
        public StudentClassRepository(IDatabaseHelper dbHelper)
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
        public List<StudentClassModel> Search(int pageIndex, int pageSize, string class_id, string student_name,out long total)
        {
            total = 0;
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@page_index", DbType.Int32, pageIndex),
                    _dbHelper.CreateInParameter("@page_size", DbType.Int32,  pageSize),
                    _dbHelper.CreateInParameter("@class_id", DbType.String,  class_id),
                    _dbHelper.CreateInParameter("@student_name", DbType.String,  student_name),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<StudentClassModel>("dbo.teacher_student_class_search", parameters);
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
        /// Update information in the tableEvaluateRecruitment
        /// </summary>
        /// <param name="model">the record updated</param>
        /// <returns></returns>
        public bool Update(StudentClassModel model)
        {
            try
            {

                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@student_id_rcd",DbType.String,model.student_id_rcd),
                    _dbHelper.CreateInParameter("@student_name",DbType.String,model.student_name),
                    _dbHelper.CreateInParameter("@gender",DbType.Boolean,model.gender),
                    _dbHelper.CreateInParameter("@date_of_birth",DbType.String,model.date_of_birth),
                    _dbHelper.CreateInParameter("@student_address",DbType.String,model.student_address),
                    _dbHelper.CreateInParameter("@student_email",DbType.String,model.student_email),
                    _dbHelper.CreateInParameter("@phone_number",DbType.String,model.phone_number),
                    _dbHelper.CreateInParameter("@class_id",DbType.String,model.class_id),
                    _dbHelper.CreateInParameter("@password",DbType.String,model.password),
                    _dbHelper.CreateInParameter("@student_status",DbType.String,model.student_status),
                    _dbHelper.CreateInParameter("@student_role",DbType.String,model.student_role),
                    _dbHelper.CreateInParameter("@created_by_user_id",DbType.Guid,model.created_by_user_id),    
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.teacher_student_class_update", parameters);
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

    }
}
