using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public partial class TuitionFeeModel
    {
        public Guid tuition_fee_id { get; set; }
        public string tuition_academy_year { get; set; }
        public int tuition_semester { get; set; }
        public int tuition_fee { get; set; }
        public int active_flag { get; set; }
        public string class_id { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
    }
    public partial class StudentTuitionFeeModel
    {
        public Guid student_tuition_fee_id { get; set; }
        public Guid tuition_fee_id { get; set; }
        public string student_rcd { get; set; }
        public int active_flag { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
    }
    public partial class TuitionFeeSearchModel
    {
        public long RowNumber { get; set; }
        public string student_rcd { get; set; }
        public string student_name { get; set; }
        public Guid? student_tuition_fee_id { get; set; }
        public Guid tuition_fee_id { get; set; }
        public string tuition_academy_year { get; set; }
        public int tuition_semester { get; set; }
        public int tuition_fee { get; set; }
        public bool is_paid { get; set; }
    }
    public partial class TuitionFeeRequest
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string academy_year { get; set; }
        public string semester { get; set; }
        public string student_rcd { get; set; }
        public string class_id { get; set; }
    }
    public partial class TuitionFeeMainRequest
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string academy_year { get; set; }
        public string semester { get; set; }
        public string class_id { get; set; }
    }
}
