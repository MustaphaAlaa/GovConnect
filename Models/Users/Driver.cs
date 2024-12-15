using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Users;

public class Driver
{
    [Key] public Guid DriverId { get; set; }

    [ForeignKey("user")] [Required] public Guid UserId { get; set; }

    [ForeignKey("employee")] [Required] public Guid CreatedByEmployee { get; set; }

    public DateTime CreatedDate { get; set; }


    public User user { get; set; }
    public Employee employee { get; set; }
}