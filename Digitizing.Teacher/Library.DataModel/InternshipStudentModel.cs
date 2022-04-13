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
        public string gender { get; set; }
        public DateTime date_of_birth { get; set; }
        public int student_email { get; set; }
        public string company_name { get; set; }
        public string course_year { get; set; }
    }
}
