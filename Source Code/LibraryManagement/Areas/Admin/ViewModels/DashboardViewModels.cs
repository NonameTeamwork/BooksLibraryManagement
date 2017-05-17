using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Areas.Admin.ViewModels
{
    public class QuickEmailViewModels
    {
        public string RecipientEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
