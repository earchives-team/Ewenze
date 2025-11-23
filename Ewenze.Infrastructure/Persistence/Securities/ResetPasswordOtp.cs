using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Infrastructure.Persistence.Securities
{
    public class ResetPasswordOtp
    {
       public string UserId { get; set; }
       public string OtpCodeHash { get; set; }
       public DateTime ExpiryTime { get; set; }
       public bool IsUsed { get; set; }
       public DateTime CreationDate { get; set; }
    }
}
