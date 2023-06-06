using Library.DataAccessLayer;
using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public class MessageBusiness : IMessageBusiness
    {
        private IMessageRepository _res;
        public MessageBusiness(IMessageRepository res)
        {
            _res = res;
        }
        public List<UserInChatModel> SearchListUser(UserInChatRequest request, out long total)
        {
            return _res.SearchListUser(request, out total);
        }
        public List<UserInChatModel> SearchListTeacher(UserInChatRequest request, out long total)
        {
            return _res.SearchListTeacher(request, out total);
        }
        public List<UserInChatModel> SearchListParent(UserInChatRequest request, out long total)
        {
            return _res.SearchListParent(request, out total);
        }
    }
}
