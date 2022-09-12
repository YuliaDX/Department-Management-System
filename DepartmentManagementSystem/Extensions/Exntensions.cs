using Core.Domain;

namespace DepartmentManagementSystem.Extensions
{
    public static class Exntensions
    {
		public static Department BypassTrees(this Department node, Action<Department> nodeAction,
			Func<Department, bool> stopCondition = null)
		
		{
			var stack = new Stack<Department>();
			//foreach (var node in nodes)
			{
				stack.Push(node);
				while (stack.Any())
				{
					Department currentFolder = stack.Pop();
					if (currentFolder.SubDepartments.Any())
					{
						for (int i = currentFolder.SubDepartments.Count - 1; i >= 0; i--)
						{
							Department subFolder = currentFolder.SubDepartments.ElementAt(i);
							stack.Push(subFolder);
						}
					}
					nodeAction.Invoke(currentFolder);
					if (stopCondition != null)
					{
						if (stopCondition(currentFolder))
						{
							return node;
						}
					}
				}
			}
			return node;
		}
	}

}
