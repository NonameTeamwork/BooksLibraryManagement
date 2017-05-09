using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    interface IEmailSender
    {
        Task SendEmail(string email, string subject, string htmlmessage, string txtmessage);
    }
}
