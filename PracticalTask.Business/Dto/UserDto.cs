namespace PracticalTask.Business.Dto
{
    public class UserDto : IUserDto
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public bool UserIsActive { get; set; } = true;
    }
}
