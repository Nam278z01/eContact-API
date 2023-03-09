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
        string Create(TuitionFeeModel model);
        bool Update(TuitionFeeModel model);
        List<TuitionFeeSearchModel> Search(TuitionFeeRequestModel request, out long total);
    }
}
