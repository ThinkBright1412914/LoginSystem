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

        public const string ActivateCode = ApiVerion + "Register/ActivationCode";

        public const string ResetPassword = ApiVerion + "Login/ResetPassword";

        public const string ForcePasswordReset = ApiVerion + "Login/ForcePasswordReset";

        public const string ForgotPassword = ApiVerion + "Login/ForgotPassword";

        public const string ForgotPasswordConfirm = ApiVerion + "Login/ForgotPasswordConfirm";

		public const string EditUser = ApiVerion + "Login/EditUser";

		public const string GetUsers = ApiVerion + "User/GetUsers";

        public const string CreateUser = ApiVerion + "User/CreateUser";

        public const string UpdateUser = ApiVerion + "User/UpdateUser";

        public const string DeleteUser = ApiVerion + "User/DeleteUser";

        public const string GetUserById = ApiVerion + "User/GetUserById";
    }
}
