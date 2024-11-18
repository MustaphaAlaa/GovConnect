using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Users;

namespace Models.Applications;

public class Application
{
    [Key] public int Id { get; set; }

    [Required][ForeignKey("User")] public Guid ApplicantUserId { get; set; }

    public int ApplicationStatus { get; set; }

    public DateTime ApplicationDate { get; set; }

    public DateTime LastStatusDate { get; set; }

    public decimal PaidFees { get; set; }

    [Required] public int ApplicationTypeId { get; set; }
    [Required] public int ApplicationForId { get; set; }

    [ForeignKey("Employee")] public Guid CreatedByEmployeeId { get; set; }

    public User User { get; set; }
    public ApplicationFees ApplicationFees { get; set; }
    public Employee Employee { get; set; }
}