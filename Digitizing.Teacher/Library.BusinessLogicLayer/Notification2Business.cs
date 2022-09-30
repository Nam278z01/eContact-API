using System;
using System.Collections.Generic;
using Library.DataModel;
using Library.DataAccessLayer;
using System.Linq;
using System.Data;

namespace Library.BusinessLogicLayer
{
    public class Notification2Business : INotification2Business
    {
        private INotification2Repository _res;
        public Notification2Business(INotification2Repository res)
        {
            _res = res;
        }
        /// <summary>
        /// Add a new records into the table WebsiteTag 
        /// </summary>
        /// <param name="model">the record added </param>
        /// <returns></returns>
        public bool Create(NotificationInfoModel model)
        {
            return _res.Create(model);
        }
        public List<NotificationInfoSearchModel> Search(NotificationInfoRequest request, out long total)
        {
            return _res.Search(request, out total);
        }
        public List<NotificationInfoModel> Delete(string json_list_id, Guid updated_by)
        {
            return _res.Delete(json_list_id, updated_by);
        }

        public NotificationInfoModel GetById(Guid? id)
        {
            return _res.GetById(id);
        }
        public List<DropdownOptionModel> GetParentsListDropdownByClass(string class_id)
        {
            return _res.GetParentsListDropdownByClass(class_id);
        }
        public List<Notification2SearchModel> GetMyNotification(Notification2Request request, out long total)
        {
            return _res.GetMyNotification(request, out total);
        }
        public Notification2SearchModel GetMyNotificationById(Guid? id, Guid user_id)
        {
            return _res.GetMyNotificationById(id, user_id);
        }
        public bool CheckNotification(Guid user_id)
        {
            return _res.CheckNotification(user_id);
        }
        public int CountUnCheckedNotification(Guid user_id)
        {
            return _res.CountUnCheckedNotification(user_id);
        }
    }
}
