using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public partial class ScientificResearchModel
    {
        public string scientific_research_id {get; set;}
        public string category_type_id       {get; set;}
        public string student_id             {get; set;}
        public string teacher_id             {get; set;}
        public string research_project_name  {get; set;}
        public string content                {get; set;}
        public string link_file              {get; set;}
        public DateTime? report_date            {get; set;}
        public DateTime start_date             {get; set;}
        public string rate_id                {get; set;}
        public string rank { get; set; }
        public int active_flag { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
    }

    public partial class ScientificResearchSearchModel
    {
        public string scientific_research_id { get; set; }
        public string category_type_name { get; set; }
        public string student_name { get; set; }
        public string teacher_name { get; set; }
        public string research_project_name { get; set; }
        public string content { get; set; }
        public string link_file { get; set; }
        public DateTime? report_date { get; set; }
        public DateTime start_date { get; set; }
        public string rank { get; set; }
        public string rate_content { get; set; }
        public int active_flag { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
    }
}
