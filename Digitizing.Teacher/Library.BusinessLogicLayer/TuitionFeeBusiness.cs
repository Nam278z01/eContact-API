using Library.DataAccessLayer;
using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public partial class TuitionFeeBusiness : ITuitionFeeBusiness
    {
        private ITuitionFeeRepository _res;
        public TuitionFeeBusiness(ITuitionFeeRepository res)
        {
            _res = res;
        }
        public bool Create(TuitionFeeModel model)
        {
            return _res.Create(model);
        }
        public bool CreateStudentTuitionFee(StudentTuitionFeeModel model)
        {
            return _res.CreateStudentTuitionFee(model);
        }
        public bool Update(TuitionFeeModel model)
        {
            return _res.Update(model);
        }
        public List<TuitionFeeSearchModel> Search(TuitionFeeRequest request, out long total)
        {
            return _res.Search(request, out total);
        }
        public List<TuitionFeeModel> SearchTuitionFeeMain(TuitionFeeMainRequest request, out long total)
        {
            return _res.SearchTuitionFeeMain(request, out total);
        }
        public List<TuitionFeeModel> Delete(string json_list_id, Guid updated_by)
        {
            return _res.Delete(json_list_id, updated_by);
        }
        public List<StudentTuitionFeeModel> DeleteStudentTuitionFee(string json_list_id, Guid updated_by)
        {
            return _res.DeleteStudentTuitionFee(json_list_id, updated_by);
        }
    }
}
