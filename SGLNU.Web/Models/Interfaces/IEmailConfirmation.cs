using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuLNU.Web.Models.Interfaces
{
    public interface IEmailConfirmation
    {
        Task SendEmailAsync(string email, string subject, string message, string title);
    }
}
