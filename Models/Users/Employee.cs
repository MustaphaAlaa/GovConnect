using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Types;

namespace Models.Users;

public class Employee
{
    [Key] public Guid Id { get; set; }

    [ForeignKey("user")] [Required] public Guid UserId { get; set; }

    [ForeignKey("Admin")] [Required] public Guid HiredByAdmin { get; set; }

    public DateTime HiredDate { get; set; }

    /*[ForeignKey("EmpType")] public int EmployeeTypeId { get; set; }*/


    public User user { get; set; }
    public Admin Admin { get; set; }
    /*public EmployeeType EmpType { get; set; }*/
}