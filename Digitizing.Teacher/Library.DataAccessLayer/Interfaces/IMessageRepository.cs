using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer
{
    public interface IMessageRepository
    {
        List<UserInChatModel> SearchListUser(UserInChatRequest request, out long total);
        List<UserInChatModel> SearchListTeacher(UserInChatRequest request, out long total);
        List<UserInChatModel> SearchListParent(UserInChatRequest request, out long total);
    }
}
