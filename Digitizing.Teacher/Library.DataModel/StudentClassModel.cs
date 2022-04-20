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
        public string student_id_rcd { get; set; }
        public string student_name { get; set; }
        public string class_name { get; set; }
        public string gender { get; set; }
        public string Phone_number { get; set; }
        public string student_email { get; set; }
        public string student_address { get; set; }
        public DateTime date_of_birth { get; set; }
        public string class_role { get; set; }

    }
}
