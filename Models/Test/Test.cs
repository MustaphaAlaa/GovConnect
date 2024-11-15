using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Users;

namespace Models.Test;

public class Test
{
    [Key] public int Id { get; set; }

    [Required]
    [ForeignKey("TestAppointment")]
    public int TestAppointmentId { get; set; }

    [Required] public bool TestResult { get; set; }
    public string Notes { get; set; }

    [Required] [ForeignKey("Employe")] public Guid CreatedByEmployee { get; set; }

    public Employee Employee { get; set; }
    public TestAppointment TestAppointment { get; set; }
}