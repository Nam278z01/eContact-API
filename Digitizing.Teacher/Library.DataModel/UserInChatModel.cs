using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataModel
{
    public partial class UserInChatModel
    {
        public long RowNumber { get; set; }
        public Guid person_id { get; set; }
        public string description_l { get; set; }
        public bool online_flag { get; set; }
        public string full_name { get; set; }
        public string avatar { get; set; }
    }
    public partial class UserInChatRequest
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string text_search { get; set; }
        public Guid? user_id { get; set; }
    }
}
