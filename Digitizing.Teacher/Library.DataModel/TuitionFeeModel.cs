using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public class TuitionFeeModel
    {
        public Guid tuition_fee_id { get; set; }
        public string student_rcd { get; set; }
        public string academy_year { get; set; }
        public int semester { get; set; }
        public int? owe_tuition_fee_last_semester { get; set; }
        public int? owe_TATC_last_semester { get; set; }
        public int? tuition_fee_to_be_paid { get; set; }
        public int? TATC_to_be_paid { get; set; }
        public int? tuition_fee_exemption { get; set; }
        public int? tuition_fee_paid { get; set; }
        public int? refund_of_tuition_fee { get; set; }
        public int? TATC_paid { get; set; }
        public int? lack_or_excess_of_TATC { get; set; }
        public int? lack_or_excess_of_tuition_fee { get; set; }
        public string note { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
        public int active_flag { get; set; }
    }
    public class TuitionFeeSearchModel
    {
        public long RowNumber { get; set; }
        public Guid tuition_fee_id { get; set; }
        public string student_rcd { get; set; }
        public string student_name { get; set; }
        public string academy_year { get; set; }
        public int semester { get; set; }
        public int? owe_tuition_fee_last_semester { get; set; }
        public int? owe_TATC_last_semester { get; set; }
        public int? tuition_fee_to_be_paid { get; set; }
        public int? TATC_to_be_paid { get; set; }
        public int? tuition_fee_exemption { get; set; }
        public int? tuition_fee_paid { get; set; }
        public int? refund_of_tuition_fee { get; set; }
        public int? TATC_paid { get; set; }
        public int? lack_or_excess_of_TATC { get; set; }
        public int? lack_or_excess_of_tuition_fee { get; set; }
        public string note { get; set; }
        public string class_id { get; set; }
    }
    public class TuitionFeeRequestModel
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string student_rcd { get; set; }
        public string academy_year { get; set; }
        public string semester { get; set; }
        public bool? is_paid { get; set; }
        public string class_id { get; set; }
    }
}
