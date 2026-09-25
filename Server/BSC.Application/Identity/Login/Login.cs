using System;
using System.Collections.Generic;
using System.Text;

namespace BSC.Application.Identity.Login
{
    public class Login
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = [];
    }
}
