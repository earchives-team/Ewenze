using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Models.AuthModel
{
    public class AuthRequest
    {
        /// <summary>
        /// Represent The Email or the Password to authenticate the user 
        /// </summary>
        public string? LoginInformation { get; set; }
        public string Password { get; set; }
    }
}
