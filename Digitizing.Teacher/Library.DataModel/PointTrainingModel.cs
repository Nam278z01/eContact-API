using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public partial class PointTrainingModel
    {
        public Guid point_training_id { get; set; }
        public string academy_year { get; set; }
        public int semester { get; set; }
        public int point { get; set; }
        public string student_rcd { get; set; }
        public string class_id { get; set; }
        public int active_flag { get; set; }
        public Guid created_by_user_id { get; set; }
        public DateTime created_date_time { get; set; }
        public DateTime? lu_updated { get; set; }
        public Guid? lu_user_id { get; set; }
    }
    public partial class PointTrainingSearchModel
    {
        public long RowNumber { get; set; }
        public Guid point_training_id { get; set; }
        public string academy_year { get; set; }
        public int semester { get; set; }
        public int point { get; set; }
        public string student_rcd { get; set; }
        public string student_name { get; set; }
        public string class_id { get; set; }
    }
}
