using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{

    public class StudentClassModel
    {
        public long RowNumber { get; set; }
        public string student_rcd { get; set; }
        public string student_name { get; set; }
        public bool gender { get; set; }
        public string student_phone { get; set; }
        public string class_id { get; set; }
        public string student_email { get; set; }
        public string student_resident_ward { get; set; }
        public DateTime date_of_birth { get; set; }
        public string student_role { get; set; }
        public string student_status { get; set; }

    }
}
