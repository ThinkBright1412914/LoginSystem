using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Utility
{
    public static class ApiUri
    {

        public const string ApiVerion = "/api/";

        public const string Login = ApiVerion + "Login/Authenticate";

        public const string Reigster = ApiVerion + "Register/SignUp";

        public const string ActivateCode = ApiVerion + "Register/ActivateCode";
    }
}
