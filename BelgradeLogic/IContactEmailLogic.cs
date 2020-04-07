using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BelgradeLogic
{
    public interface IContactEmailLogic
    {
        Task<bool> SendEmail(ContactEmail email);
    }
}
