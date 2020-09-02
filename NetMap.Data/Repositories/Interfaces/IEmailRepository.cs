using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetMap.Data.Repositories.Interfaces
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
