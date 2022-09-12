namespace Core.Domain
{
    public class Department:BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }

        public Department ParentDepartment { get; set; }
        public int? ParentDepartmentId { get; set; }
        public virtual ICollection<Department> SubDepartments { get; set; }
    }
}