namespace FA20.P05.Web.Features.StaffMembers
{
    public class CreateStaffDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int[] Schools { get; set; }
    }
}