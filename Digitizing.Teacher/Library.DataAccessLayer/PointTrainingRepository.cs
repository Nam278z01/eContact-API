using System;
using System.Collections.Generic;
using Library.DataModel;
using Library.Common.Helper;
using System.Data;
using System.Linq;

namespace Library.DataAccessLayer
{
    public partial class PointTrainingRepository : IPointTrainingRepository
    {
        private IDatabaseHelper _dbHelper;
        public PointTrainingRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(PointTrainingModel model)
        {
            try
            {
                if (model.point_training_id == Guid.Empty) model.point_training_id = Guid.NewGuid();
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@point_training_id", DbType.Guid, model.point_training_id),
                    _dbHelper.CreateInParameter("@academy_year", DbType.String, model.academy_year),
                    _dbHelper.CreateInParameter("@semester", DbType.Int32, model.semester),
                    _dbHelper.CreateInParameter("@point", DbType.Int32, model.point),
                    _dbHelper.CreateInParameter("@student_rcd", DbType.String, model.student_rcd),
                    _dbHelper.CreateInParameter("@created_by_user_id", DbType.Guid, model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.teacher_point_training_create", parameters);
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

        public bool Update(PointTrainingModel model)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@point_training_id", DbType.Guid, model.point_training_id),
                    _dbHelper.CreateInParameter("@academy_year", DbType.String, model.academy_year),
                    _dbHelper.CreateInParameter("@semester", DbType.Int32, model.semester),
                    _dbHelper.CreateInParameter("@point", DbType.Int32, model.point),
                    _dbHelper.CreateInParameter("@student_rcd", DbType.String, model.student_rcd),
                    _dbHelper.CreateInParameter("@lu_user_id", DbType.Guid, model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.teacher_point_training_update", parameters);
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

        public List<PointTrainingSearchModel> Search(PointTrainingRequest request, out long total)
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
                    _dbHelper.CreateInParameter("@student_rcd", DbType.String,  request.student_rcd),
                    _dbHelper.CreateInParameter("@student_name", DbType.String,  request.student_name),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<PointTrainingSearchModel>("dbo.teacher_point_training_search", parameters);
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

        public PointTrainingModel GetById(Guid? id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@point_training_id",DbType.Guid, id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToFirstOrDefault<PointTrainingModel>("dbo.teacher_point_training_get_by_id", parameters);
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

        public List<PointTrainingModel> Delete(string json_list_id, Guid updated_by)
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
                var result = _dbHelper.CallToList<PointTrainingModel>("dbo.teacher_point_training_delete_multi", parameters);
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
