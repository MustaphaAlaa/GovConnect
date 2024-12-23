using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Users;

public class Admin
{
    [Key] public Guid Id { get; set; }

    [ForeignKey("user")] public Guid UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsEmployee { get; set; }
    public User user { get; set; }
    public IEnumerable<Employee> employees { get; set; }


}