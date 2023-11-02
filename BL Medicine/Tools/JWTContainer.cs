using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace BL_Medicine.Tools
{
    public class JWTContainer
    {
        public int ExpireMinutes { get; set; } = 10800; // 7 days
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public Claim[]? Claims { get; set; }
    }
}
