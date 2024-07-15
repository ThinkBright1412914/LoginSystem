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

        public const string CreateRole = ApiVerion + "Role/CreateRole";

        public const string GetRoles = ApiVerion + "Role/GetRoles";

        public const string DeleteRole = ApiVerion + "Role/DeleteRole";

        public const string GetCarousels = ApiVerion + "Carousel/GetCarousels";

        public const string CreateCarousel = ApiVerion + "Carousel/CreateCarousel";

        public const string DeleteCarousel = ApiVerion + "Carousel/DeleteCarousel";

        public const string GetIndustrys = ApiVerion + "Industry/GetIndustry";

        public const string CreateIndustry = ApiVerion + "Industry/CreateIndustry";

        public const string DeleteIndustry = ApiVerion + "Industry/DeleteIndustry";

        public const string GetLanguages = ApiVerion + "Language/GetLanguages";

        public const string CreateLanguage = ApiVerion + "Language/CreateLanguage";

        public const string DeleteLanguage = ApiVerion + "Language/DeleteLanguage";

        public const string GetGenres = ApiVerion + "Genre/GetGenres";

        public const string CreateGenre = ApiVerion + "Genre/CreateGenre";

        public const string DeleteGenre = ApiVerion + "Genre/DeleteGenre";
    }
}
