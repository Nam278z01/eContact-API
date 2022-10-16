using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public interface ITuitionFeeBusiness
    {
        bool Create(TuitionFeeModel model);
        bool CreateStudentTuitionFee(StudentTuitionFeeModel model);
        bool Update(TuitionFeeModel model);
        List<TuitionFeeSearchModel> Search(TuitionFeeRequest request, out long total);
        List<TuitionFeeModel> Delete(string json_list_id, Guid updated_by);
        List<StudentTuitionFeeModel> DeleteStudentTuitionFee(string json_list_id, Guid updated_by);
        List<TuitionFeeModel> SearchTuitionFeeMain(TuitionFeeMainRequest request, out long total);
    }
}
