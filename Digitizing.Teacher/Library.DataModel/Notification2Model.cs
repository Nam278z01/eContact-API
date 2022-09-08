using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public partial class NotificationInfoModel
    {
        public Guid notification_info_id { get; set; }
        public string notification_title { get; set; }
        public string notification_content { get; set; }
        public string class_id { get; set; }
        public List<string> receivers { get; set; }
        public int active_flag { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
    }
    public partial class NotificationInfoSearchModel
    {
        public Guid notification_info_id { get; set; }
        public string notification_title { get; set; }
        public string notification_content { get; set; }
        public string class_id { get; set; }
        public List<Notification2SearchModel> receivers { get; set; }
    }
    public partial class Notification2Model
    {
        public Guid notification_id { get; set; }
        public Guid receiver_id { get; set; }
        public bool is_read { get; set; }
        public bool is_checked { get; set; }
        public Guid notification_info_id { get; set; }
        public int active_flag { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
    }
    public partial class Notification2SearchModel
    {
        public Guid notification_id { get; set; }
        public Guid receiver_id { get; set; }
        public Guid sender_id { get; set; }
        public Guid sender_name { get; set; }
        public bool is_read { get; set; }
        public bool is_checked { get; set; }
        public Guid notification_info_id { get; set; }
        public string notification_title { get; set; }
        public string notification_content { get; set; }
        public string notification_type { get; set; }
    }
    public partial class NotificarionInfoRequest
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string class_id { get; set; }
        public Guid user_id { get; set; }
    }
}
