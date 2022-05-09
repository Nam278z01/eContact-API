using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{

    public class StudentRefModel
    {
        public string student_rcd { get; set; }
        public string student_name { get; set; }
        public bool gender { get; set; }
        public string student_address { get; set; }
        public string student_email { get; set; }
        public string student_phone { get; set; }
        public string class_id { get; set; }
        public string student_status { get; set; }
        public string student_role { get; set; }
        public string password { get; set; }
        public int priority_flag { get; set; }
        public Guid lu_user_id { get; set; }

    }
}
