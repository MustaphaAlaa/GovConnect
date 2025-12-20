using System.ComponentModel.DataAnnotations;

namespace ModelDTO.Users;

public class DriverDTO
{
    [Key] public Guid DriverId { get; set; }

    [Required] public Guid UserId { get; set; }

    [Required] public Guid CreatedByEmployee { get; set; }

    public DateTime CreatedDate { get; set; }
 
}