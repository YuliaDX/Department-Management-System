namespace Core.Domain
{
    public class Department:BaseEntity
    {
        public string Name { get; set; }
        public List<User> Users { get; set; }

        public Department ParentDepartment { get; set; }
    }
}