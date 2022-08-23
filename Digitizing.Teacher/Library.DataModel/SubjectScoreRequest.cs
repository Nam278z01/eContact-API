using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public partial class SubjectScoreRequest
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string subject_id { get; set; }
        public string subject_name { get; set; }
        public string academy_year { get; set; }
        public string semester { get; set; }
        public string student_rcd { get; set; }
        public string student_name { get; set; }
        public string class_id { get; set; }
    }
}
