using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public partial class ReportRecruitmentModel
    {
      public string student_rcd { get; set; }  
      public string student_name { get; set; }
      public string week_name { get; set; }
      public string class_id { get; set; }
      public int progress { get; set; }
        public int active_flag { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
    }
}
