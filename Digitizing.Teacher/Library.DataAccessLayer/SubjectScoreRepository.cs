using System;
using System.Collections.Generic;
using Library.DataModel;
using Library.Common.Helper;
using System.Data;
using System.Linq;

namespace Library.DataAccessLayer
{
    public partial class SubjectScoreRepository : ISubjectScoreRepository
    {
        private IDatabaseHelper _dbHelper;
        //DatabaseHelper dbHelper;
        public SubjectScoreRepository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(SubjectScoreModel model)
        {
            try
            {
                if(model.subject_score_id == Guid.Empty) model.subject_score_id = Guid.NewGuid();
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@subject_score_id", DbType.Guid, model.subject_score_id),
                    _dbHelper.CreateInParameter("@subject_id", DbType.String, model.subject_id),
                    _dbHelper.CreateInParameter("@subject_name", DbType.String, model.subject_name),
                    _dbHelper.CreateInParameter("@score", DbType.Double, model.score),
                    _dbHelper.CreateInParameter("@score1", DbType.Double, model.score1),
                    _dbHelper.CreateInParameter("@score2", DbType.Double, model.score2),
                    _dbHelper.CreateInParameter("@number_credits", DbType.Int32, model.number_credits),
                    _dbHelper.CreateInParameter("@academy_year", DbType.String, model.academy_year),
                    _dbHelper.CreateInParameter("@semester", DbType.Int32, model.semester),
                    _dbHelper.CreateInParameter("@note", DbType.String, model.note),
                    _dbHelper.CreateInParameter("@student_rcd", DbType.String, model.student_rcd),
                    _dbHelper.CreateInParameter("@created_by_user_id", DbType.Guid, model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.teacher_subject_score_create", parameters);
                if((result != null) && !string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
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

        public bool Update(SubjectScoreModel model)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@subject_score_id", DbType.Guid, model.subject_score_id),
                    _dbHelper.CreateInParameter("@subject_id", DbType.String, model.subject_id),
                    _dbHelper.CreateInParameter("@subject_name", DbType.String, model.subject_name),
                    _dbHelper.CreateInParameter("@score", DbType.Double, model.score),
                    _dbHelper.CreateInParameter("@score1", DbType.Double, model.score1),
                    _dbHelper.CreateInParameter("@score2", DbType.Double, model.score2),
                    _dbHelper.CreateInParameter("@number_credits", DbType.Int32, model.number_credits),
                    _dbHelper.CreateInParameter("@academy_year", DbType.String, model.academy_year),
                    _dbHelper.CreateInParameter("@semester", DbType.Int32, model.semester),
                    _dbHelper.CreateInParameter("@note", DbType.String, model.note),
                    _dbHelper.CreateInParameter("@student_rcd", DbType.String, model.student_rcd),
                    _dbHelper.CreateInParameter("@lu_user_id", DbType.Guid, model.lu_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.teacher_subject_score_update", parameters);
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

        public List<SubjectScoreSearchModel> Search(SubjectScoreRequest request, out long total)
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
                    _dbHelper.CreateInParameter("@subject_id", DbType.String,  request.subject_id),
                    _dbHelper.CreateInParameter("@subject_name", DbType.String,  request.subject_name),
                    _dbHelper.CreateInParameter("@student_rcd", DbType.String,  request.student_rcd),
                    _dbHelper.CreateInParameter("@student_name", DbType.String,  request.student_name),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<SubjectScoreSearchModel>("dbo.teacher_subject_score_search", parameters);
                if(!string.IsNullOrEmpty(result.ErrorMessage) && result.ErrorCode != 0)
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

        public List<StudentForSubjectScore> GetStudentsByFamily(Guid user_id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@user_id", DbType.Guid, user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<StudentForSubjectScore>("dbo.teacher_student_get_list_by_family", parameters);
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

        public SubjectScoreModel GetById (Guid? id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@subject_score_id",DbType.Guid, id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToFirstOrDefault<SubjectScoreModel>("dbo.teacher_subject_score_get_by_id", parameters);
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

        public List<SubjectScoreModel> Delete(string json_list_id, Guid updated_by)
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
                var result = _dbHelper.CallToList<SubjectScoreModel>("dbo.teacher_subject_score_delete_multi", parameters);
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

        public List<DropdownOptionModel> GetSubjectListDropdown(string class_id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@class_id", DbType.String, class_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<DropdownOptionModel>("dbo.teacher_subject_get_list_dropdown", parameters);
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

        public List<DropdownOptionModel> GetAcademyYearListDropdown(string class_id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@class_id", DbType.String, class_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<DropdownOptionModel>("dbo.teacher_academy_year_get_list_dropdown", parameters);
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
        public List<DropdownOptionModel> GetStudentListDropdown(string class_id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@class_id", DbType.String, class_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<DropdownOptionModel>("dbo.teacher_student_sj_get_list_dropdown", parameters);
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
