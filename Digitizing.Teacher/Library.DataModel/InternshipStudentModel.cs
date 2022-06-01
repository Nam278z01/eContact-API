using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{

    public class InternshipStudentSearchModel
    {
        public long RowNumber { get; set; }
        public string student_rcd { get; set; }
        public string student_name { get; set; }
        public string class_name { get; set; }
        public bool gender { get; set; }
        public DateTime date_of_birth { get; set; }
        public string student_email { get; set; }
        public string company_name { get; set; }
        public string course_year { get; set; }
        public Guid? evaluate_recruitment_rcd { get; set; }
        public double point { get; set; }
        public double sumpoint { get; set; }
        public string class_id_rcd { get; set; }
        public string school_year { get; set; }
    }
}
