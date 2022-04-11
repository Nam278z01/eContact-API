using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public partial class InternshipClassModel
    {
        public string internship_class_id { get; set; }
        public string class_id_rcd { get; set; }
        public string internship_id_rcd { get; set; }
        public int active_flag { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
    }

    public class InternshipClassSearchModel
    {
        public long RowNumber { get; set; }
        public string internship_class_id { get; set; }
        public string class_id_rcd { get; set; }
        public string class_name { get; set; }
        public string internship_id_rcd { get; set; }
        public string academic_year { get; set; }
        public int semester { get; set; }
        public string internship_name { get; set; }
        public DateTime start_date { get; set; }
        public int number_of_weeks { get; set; }
    }
}
