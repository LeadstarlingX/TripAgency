using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public enum ErrorResponseCode
    {
        VerifyEmailCode = 1,
        CompleteRegister = 4,
        BannedUser = 8,
        DeletedUser = 9,
    }
}