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
    public class TuitionFeeRepository : ITuitionFeeRepository
    {
        private IDatabaseHelper _dbHelper;
        public TuitionFeeRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }
        public string Create(TuitionFeeModel model)
        {
            try
            {

                if (model.tuition_fee_id == Guid.Empty) model.tuition_fee_id = Guid.NewGuid();
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@tuition_fee_id",DbType.Guid,model.tuition_fee_id),
                    _dbHelper.CreateInParameter("@student_rcd",DbType.String,model.student_rcd),
                    _dbHelper.CreateInParameter("@academy_year",DbType.String,model.academy_year),
                    _dbHelper.CreateInParameter("@semester",DbType.Int32,model.semester),
                    _dbHelper.CreateInParameter("@owe_tuition_fee_last_semester",DbType.Int32,model.owe_tuition_fee_last_semester),
                    _dbHelper.CreateInParameter("@owe_TATC_last_semester",DbType.Int32,model.owe_TATC_last_semester),
                    _dbHelper.CreateInParameter("@tuition_fee_to_be_paid",DbType.Int32,model.tuition_fee_to_be_paid),
                    _dbHelper.CreateInParameter("@TATC_to_be_paid",DbType.Int32,model.TATC_to_be_paid),
                    _dbHelper.CreateInParameter("@tuition_fee_exemption",DbType.Int32,model.tuition_fee_exemption),
                    _dbHelper.CreateInParameter("@tuition_fee_paid",DbType.Int32,model.tuition_fee_paid),
                    _dbHelper.CreateInParameter("@refund_of_tuition_fee",DbType.Int32,model.refund_of_tuition_fee),
                    _dbHelper.CreateInParameter("@TATC_paid",DbType.Int32,model.TATC_paid),
                    _dbHelper.CreateInParameter("@lack_or_excess_of_TATC",DbType.Int32,model.lack_or_excess_of_TATC),
                    _dbHelper.CreateInParameter("@lack_or_excess_of_tuition_fee",DbType.Int32,model.lack_or_excess_of_tuition_fee),
                    _dbHelper.CreateInParameter("@note",DbType.String,model.note),

                    _dbHelper.CreateInParameter("@created_by_user_id",DbType.Guid,model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction<string>("dbo.[parent_tuition_fee_create]", parameters);
                if ((result != null && !string.IsNullOrEmpty(result.ErrorMessage)) && result.ErrorCode != 0)
                {
                    throw new Exception(result.ErrorMessage);
                }
                else if (result.Value != null && result.Value.ToString().IndexOf("MESSAGE") >= 0)
                {
                    throw new Exception(result.Value.ToString());
                }
                return result.Value;
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
                    _dbHelper.CreateInParameter("@tuition_fee_id",DbType.Guid,model.tuition_fee_id),
                    _dbHelper.CreateInParameter("@student_rcd",DbType.String,model.student_rcd),
                    _dbHelper.CreateInParameter("@academy_year",DbType.String,model.academy_year),
                    _dbHelper.CreateInParameter("@semester",DbType.Int32,model.semester),
                    _dbHelper.CreateInParameter("@owe_tuition_fee_last_semester",DbType.Int32,model.owe_tuition_fee_last_semester),
                    _dbHelper.CreateInParameter("@owe_TATC_last_semester",DbType.Int32,model.owe_TATC_last_semester),
                    _dbHelper.CreateInParameter("@tuition_fee_to_be_paid",DbType.Int32,model.tuition_fee_to_be_paid),
                    _dbHelper.CreateInParameter("@TATC_to_be_paid",DbType.Int32,model.TATC_to_be_paid),
                    _dbHelper.CreateInParameter("@tuition_fee_exemption",DbType.Int32,model.tuition_fee_exemption),
                    _dbHelper.CreateInParameter("@tuition_fee_paid",DbType.Int32,model.tuition_fee_paid),
                    _dbHelper.CreateInParameter("@refund_of_tuition_fee",DbType.Int32,model.refund_of_tuition_fee),
                    _dbHelper.CreateInParameter("@TATC_paid",DbType.Int32,model.TATC_paid),
                    _dbHelper.CreateInParameter("@lack_or_excess_of_TATC",DbType.Int32,model.lack_or_excess_of_TATC),
                    _dbHelper.CreateInParameter("@lack_or_excess_of_tuition_fee",DbType.Int32,model.lack_or_excess_of_tuition_fee),
                    _dbHelper.CreateInParameter("@note",DbType.String,model.note),

                    _dbHelper.CreateInParameter("@lu_user_id",DbType.Guid,model.lu_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.[parent_tuition_fee_update]", parameters);
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
        public List<TuitionFeeSearchModel> Search(TuitionFeeRequestModel request, out long total)
        {
            total = 0;
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@page", DbType.Int32, request.page),
                    _dbHelper.CreateInParameter("@pageSize", DbType.Int32,  request.pageSize),
                    _dbHelper.CreateInParameter("@student_rcd", DbType.String,  request.student_rcd),
                    _dbHelper.CreateInParameter("@academy_year", DbType.String,  request.academy_year),
                    _dbHelper.CreateInParameter("@semester", DbType.String,  request.semester),
                    _dbHelper.CreateInParameter("@is_paid", DbType.Boolean,  request.is_paid),
                    _dbHelper.CreateInParameter("@class_id_rcd", DbType.String,  request.class_id),

                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<TuitionFeeSearchModel>("dbo.parent_tuition_fee_search", parameters);
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
    }
}
