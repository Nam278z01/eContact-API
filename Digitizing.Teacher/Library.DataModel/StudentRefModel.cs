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
        public string student_nationality { get; set; }
        public bool gender { get; set; }
        public DateTime date_of_birth { get; set; }
        public string student_religion { get; set; }
        public string student_countryside { get; set; }
        public string student_citizen_identity_card { get; set; }
        public string student_email { get; set; }
        public string student_phone { get; set; }
        public string class_id { get; set; }
        public string student_facebook_url { get; set; }
        public string father_name { get; set; }
        public string father_year_of_birth { get; set; }
        public string father_nationality { get; set; }
        public string father_nation { get; set; }
        public string father_permanent_residence { get; set; }
        public string father_work { get; set; }
        public string father_phone_number { get; set; }
        public string mother_name { get; set; }
        public string mother_year_of_birth { get; set; }
        public string mother_nationality { get; set; }
        public string mother_nation { get; set; }
        public string mother_permanent_residence { get; set; }
        public string mother_work { get; set; }
        public string mother_phone_number { get; set; }
    }
}
