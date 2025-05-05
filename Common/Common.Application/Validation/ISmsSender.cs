using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Application.Validation
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string phoneNumber, string message);
    }
}
