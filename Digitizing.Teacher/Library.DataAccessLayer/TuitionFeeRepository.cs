using Library.Common.Helper;
using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer
{
    public partial class TuitionFeeRepository : ITuitionFeeRepository
    {
        private IDatabaseHelper _dbHelper;
        //DatabaseHelper dbHelper;
        public TuitionFeeRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public bool Create(TuitionFeeModel model)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@tuition_fee_id", DbType.Guid, model.tuition_fee_id),
                    _dbHelper.CreateInParameter("@tuition_academy_year", DbType.String, model.tuition_academy_year),
                    _dbHelper.CreateInParameter("@tuition_semester", DbType.Int32, model.tuition_semester),
                    _dbHelper.CreateInParameter("@tuition_fee", DbType.Int32, model.tuition_fee),
                    _dbHelper.CreateInParameter("@class_id", DbType.String, model.class_id),
                    _dbHelper.CreateInParameter("@created_by_user_id", DbType.Guid, model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.teacher_tuition_fee_create", parameters);
                if ((result != null) && !string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
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
        public bool CreateStudentTuitionFee(StudentTuitionFeeModel model)
        {
            try
            {
                if (model.student_tuition_fee_id == Guid.Empty) model.student_tuition_fee_id = Guid.NewGuid();
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@student_tuition_fee_id", DbType.Guid, model.student_tuition_fee_id),
                    _dbHelper.CreateInParameter("@tuition_fee_id", DbType.Guid, model.tuition_fee_id),
                    _dbHelper.CreateInParameter("@student_rcd", DbType.String, model.student_rcd),
                    _dbHelper.CreateInParameter("@created_by_user_id", DbType.Guid, model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.teacher_student_tuition_fee_create", parameters);
                if ((result != null) && !string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
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

        public bool Update(TuitionFeeModel model)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                   _dbHelper.CreateInParameter("@tuition_fee_id", DbType.Guid, model.tuition_fee_id),
                    _dbHelper.CreateInParameter("@tuition_academy_year", DbType.String, model.tuition_academy_year),
                    _dbHelper.CreateInParameter("@tuition_semester", DbType.Int32, model.tuition_semester),
                    _dbHelper.CreateInParameter("@tuition_fee", DbType.Int32, model.tuition_fee),
                    _dbHelper.CreateInParameter("@class_id", DbType.String, model.class_id),
                    _dbHelper.CreateInParameter("@lu_user_id", DbType.Guid, model.lu_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.teacher_tuition_fee_update", parameters);
                if ((result != null) && !string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
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
        public List<TuitionFeeSearchModel> Search(TuitionFeeRequest request, out long total)
        {
            total = 0;
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@page", DbType.Int32, request.page),
                    _dbHelper.CreateInParameter("@pageSize", DbType.Int32,  request.pageSize),
                    _dbHelper.CreateInParameter("@academy_year", DbType.String,  request.academy_year),
                    _dbHelper.CreateInParameter("@semester", DbType.String,  request.semester),
                    _dbHelper.CreateInParameter("@class_id_rcd", DbType.String,  request.class_id),
                    _dbHelper.CreateInParameter("@student_rcd", DbType.String,  request.student_rcd),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<TuitionFeeSearchModel>("dbo.teacher_tuition_fee_search", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                if (result.Output["OUT_TOTAL_ROW"] + "" != "")
                {
                    total = Convert.ToInt32(result.Output["OUT_TOTAL_ROW"]);
                }
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TuitionFeeModel> SearchTuitionFeeMain(TuitionFeeMainRequest request, out long total)
        {
            total = 0;
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@page", DbType.Int32, request.page),
                    _dbHelper.CreateInParameter("@pageSize", DbType.Int32,  request.pageSize),
                    _dbHelper.CreateInParameter("@academy_year", DbType.String,  request.academy_year),
                    _dbHelper.CreateInParameter("@semester", DbType.String,  request.semester),
                    _dbHelper.CreateInParameter("@class_id", DbType.String,  request.class_id),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<TuitionFeeModel>("dbo.teacher_tuition_fee_main_search", parameters);
                if (!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                if (result.Output["OUT_TOTAL_ROW"] + "" != "")
                {
                    total = Convert.ToInt32(result.Output["OUT_TOTAL_ROW"]);
                }
                return result.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TuitionFeeModel> Delete(string json_list_id, Guid updated_by)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@json_list_id", DbType.String, json_list_id),
                    _dbHelper.CreateInParameter("@updated_by", DbType.Guid, updated_by),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<TuitionFeeModel>("dbo.teacher_tuition_fee_delete_multi", parameters);
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
        public List<StudentTuitionFeeModel> DeleteStudentTuitionFee(string json_list_id, Guid updated_by)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@json_list_id", DbType.String, json_list_id),
                    _dbHelper.CreateInParameter("@updated_by", DbType.Guid, updated_by),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<StudentTuitionFeeModel>("dbo.teacher_student_tuition_fee_delete_multi", parameters);
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
