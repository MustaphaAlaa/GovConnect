using System.CodeDom.Compiler;
using Models.Users;

namespace Models.Types;

public class EmployeeType
{
    public int EmployeeTypeId { get; set; }
    public int TypeName { get; set; }

    //public IEnumerable<Employee> Employee { get; set; }
}
