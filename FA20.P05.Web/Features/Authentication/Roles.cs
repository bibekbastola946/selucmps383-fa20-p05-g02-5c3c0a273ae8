namespace FA20.P05.Web.Features.Authentication
{
    public static class Roles
    {
        public const string Admin = nameof(Admin);
        public const string Principal = nameof(Principal);
        public const string Staff = nameof(Staff);

        public const string PrincipalPlus = Admin + "," + Principal;

        public const string PrincipalStaff = Staff + "," + Principal;
    }
}