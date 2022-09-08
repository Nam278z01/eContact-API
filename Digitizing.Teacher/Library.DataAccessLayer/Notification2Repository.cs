using System;
using System.Collections.Generic;
using Library.DataModel;
using Library.Common.Helper;
using System.Data;
using Library.DataAccessLayer;
using System.Linq;

namespace Library.DataAccessLayer
{
    public partial class Notification2Repository : INotification2Repository
    {
        private IDatabaseHelper _dbHelper;
        public Notification2Repository(IDatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public bool Create(NotificationInfoModel model)
        {
            try
            {
                var json_list_id = MessageConvert.SerializeObject(model.receivers.Select(ds => new { receiver_id = ds, notification_id = Guid.NewGuid() }).ToList());
                if (model.notification_info_id == Guid.Empty) model.notification_info_id = Guid.NewGuid();
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@notification_info_id", DbType.Guid, model.notification_info_id),
                    _dbHelper.CreateInParameter("@notification_title", DbType.String, model.notification_title),
                    _dbHelper.CreateInParameter("@notification_content", DbType.String, model.notification_content),
                    _dbHelper.CreateInParameter("@receivers", DbType.String,json_list_id),
                    _dbHelper.CreateInParameter("@class_id", DbType.String, model.class_id),
                    _dbHelper.CreateInParameter("@created_by_user_id", DbType.Guid, model.created_by_user_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToValueWithTransaction("dbo.teacher_notification_info_create", parameters);
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

        public List<NotificationInfoSearchModel> Search(NotificarionInfoRequest request, out long total)
        {
            total = 0;
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@page", DbType.Int32, request.page),
                    _dbHelper.CreateInParameter("@pageSize", DbType.Int32,  request.pageSize),
                    _dbHelper.CreateInParameter("@class_id", DbType.String,  request.class_id),
                    _dbHelper.CreateInParameter("@user_id", DbType.Guid,  request.user_id),
                    _dbHelper.CreateOutParameter("@OUT_TOTAL_ROW", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<NotificationInfoSearchModel>("dbo.teacher_notification_info_search", parameters);
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

        public List<DropdownOptionModel> GetParentsListDropdownByClass(string class_id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@class_id", DbType.String, class_id),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToList<DropdownOptionModel>("dbo.teacher_get_parents_list_dropdown_by_class", parameters);
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

        public NotificationInfoModel GetById(Guid? id)
        {
            try
            {
                var parameters = new List<IDbDataParameter>
                {
                    _dbHelper.CreateInParameter("@notification_info_id",DbType.Guid, id),
                    _dbHelper.CreateOutParameter("@receivers", DbType.String, 4000),
                    _dbHelper.CreateOutParameter("@OUT_ERR_CD", DbType.Int32, 10),
                    _dbHelper.CreateOutParameter("@OUT_ERR_MSG", DbType.String, 255)
                };
                var result = _dbHelper.CallToFirstOrDefault<NotificationInfoModel>("dbo.teacher_notification_info_get_by_id", parameters);
                result.Value.receivers = MessageConvert.DeserializeObject<List<string>>(result.Output["receivers"].ToString());

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

        public List<NotificationInfoModel> Delete(string json_list_id, Guid updated_by)
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
                var result = _dbHelper.CallToList<NotificationInfoModel>("dbo.teacher_notification_info_delete_multi", parameters);
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
