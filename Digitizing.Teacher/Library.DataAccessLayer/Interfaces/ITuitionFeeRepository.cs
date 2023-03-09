using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer
{
    public interface ITuitionFeeRepository
    {
        string Create(TuitionFeeModel model);
        bool Update(TuitionFeeModel model);
        List<TuitionFeeSearchModel> Search(TuitionFeeRequestModel request, out long total);
    }
}
