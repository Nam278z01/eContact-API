using System;
using System.Collections.Generic;
namespace Library.DataModel 
{
	//abc
	public partial class EvaluateRecruitmentModel
	{
		public string evaluate_recruitment_rcd { get; set; }
		public string student_rcd { get; set; }
		public double point { get; set; }
		public string academic_year { get; set; }
		public string class_id { get; set; }
		public int active_flag { get; set; }
		public DateTime created_date_time { get; set; }		
		public Guid? created_by_user_id { get; set; }

	}
}