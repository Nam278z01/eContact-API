using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public partial class RecruitmentReportModel
    {
        public Guid student_recruitment_report_id { get; set; }
        public Guid candidate_id { get; set; }
        public int report_week { get; set; }
        public int report_day { get; set; }
        public string job_assignment { get; set; }
        public string result_in_day { get; set; }
        public string description { get; set; }
        public int active_flag { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
    }

    public partial class RecruitmentReportSearchModel
    {
      public string student_rcd { get; set; }  
      public string student_name { get; set; }
      public int report_week { get; set; }
      public int number_of_weeks { get; set; }
      public string class_id { get; set; }
      public int progress { get; set; }
      public string academic_year { get; set; }
      public DateTime start_date { get; set; }
      public string company_name { get; set; }
    }
}
