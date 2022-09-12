namespace DepartmentManagementSystem.DTO
{
    public class CreateOrEditDepartmentRequest
    {
        public string Name { get; set; }
        public int? ParentDepartmentId { get; set; }
        public List<CreateUserRequest> Users { get; set; }
    }
}
