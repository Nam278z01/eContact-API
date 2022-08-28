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
                    _dbHelper.CreateInParameter("@notification_type", DbType.String, model.notification_type),
                    _dbHelper.CreateInParameter("@receiver", DbType.String, model.receiver),
                    _dbHelper.CreateInParameter("@receivers", DbType.String, json_list_id),
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
    }
}
