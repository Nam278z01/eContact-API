using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public partial class SubjectScoreModel
    {
        public Guid subject_score_id { get; set; }
        public string subject_id { get; set; }
        public string subject_name { get; set; }
        public double score { get; set; }
        public double? score1 { get; set; }
        public double? score2 { get; set; }
        public int number_credits { get; set; }
        public string academy_year { get; set; }
        public int semester { get; set; }
        public string note { get; set; }
        public string student_rcd { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }

    }
    public partial class SubjectScoreSearchModel
    {
        public long RowNumber { get; set; }
        public Guid subject_score_id { get; set; }
        public string subject_id { get; set; }
        public string subject_name { get; set; }
        public double score { get; set; }
        public double? score1 { get; set; }
        public double? score2 { get; set; }
        public int number_credits { get; set; }
        public string academy_year { get; set; }
        public int semester { get; set; }
        public string note { get; set; }
        public string student_rcd { get; set; }
        public string student_name { get; set; }
        public string class_id { get; set; }
    }
}
