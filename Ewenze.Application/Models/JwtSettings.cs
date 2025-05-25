using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ewenze.Application.Models
{
    public class JwtSettings
    {
        private string signingKey; 
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public double DurationInMinutes { get; set; }

        public string SigningKey
        {
            get => signingKey;
            set
            {
                if(string.IsNullOrWhiteSpace(value) || value.Equals("[MUSTOVERRIDE]"))
                {
                    throw new ArgumentException("Please set a signing key"); 
                }
                signingKey = value;
            }
        }
    }
}
