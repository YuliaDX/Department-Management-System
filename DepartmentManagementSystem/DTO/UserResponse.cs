namespace DepartmentManagementSystem.DTO
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public int DepartmentId { get; set; }

        public string Position { get; set; }
    }
}
