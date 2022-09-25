namespace JoyTechAPITest.Models.DataTransitionHelpers
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public RoleType Role { get; set; } = RoleType.User;
    }
    public enum RoleType
    {
        Default,
        Admin,
        User
    }
}
