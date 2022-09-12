namespace DepartmentManagementSystem.DTO
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

        public Guid DepartmentId { get; set; }

        public string Position { get; set; }
    }
}
