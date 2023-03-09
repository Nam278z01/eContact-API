using Library.DataAccessLayer;
using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public class TuitionFeeBusiness : ITuitionFeeBusiness
    {
        private ITuitionFeeRepository _res;
        public TuitionFeeBusiness(ITuitionFeeRepository res)
        {
            _res = res;
        }
        public string Create(TuitionFeeModel model)
        {
            return _res.Create(model);
        }
        public bool Update(TuitionFeeModel model)
        {
            return _res.Update(model);
        }
        public List<TuitionFeeSearchModel> Search(TuitionFeeRequestModel request, out long total)
        {
            return _res.Search(request, out total);
        }
    }
}
