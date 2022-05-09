using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public class NotificationModel
    {
		public string notification_id;
		public string notification_title;
		public string notification_content;
		public Guid notification_type_id;
		public string notification_name;
		public string address;
		public DateTime start_time;
		public DateTime end_time;
		public string file_to_send;
		public string status;
		public int active_flag;
		public Guid created_by_user_id;
		public DateTime created_date_time;
		public DateTime lu_updated;
		public Guid lu_user_id;
	}
}
