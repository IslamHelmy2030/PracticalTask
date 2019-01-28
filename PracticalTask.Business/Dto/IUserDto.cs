namespace PracticalTask.Business.Dto
{
    public interface IUserDto
    {
        int UserId { get; set; }

        string Username { get; set; }

        bool UserIsActive { get; set; }
    }
}
