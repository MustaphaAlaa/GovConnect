using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models.ApplicationModels;
using Models.Test;
using Models.Users;

namespace Models.Test;

public class TestAppointment
{
    [Key] public int Id { get; set; }
    [ForeignKey("TestType")] [Required] public int TestTypeId { get; set; }

    [ForeignKey("LocalDrivingLicenseApplication")]
    [Required]
    public int LocalDrivingLicenseApplicationId { get; set; }

    public DateTime AppointmentDate { get; set; }
    [Required] public decimal PaidFees { get; set; }

    public bool IsLocked { get; set; }

    [ForeignKey("Application")] public int RetakeTestApplicationId { get; set; }
    [ForeignKey("Employee")] [Required] public Guid CreatedByEmployeeId { get; set; }
   

    public LocalDrivingLicenseApplication LocalDrivingLicenseApplication { get; set; }
    public Employee Employee { get; set; }
    public Application Application { get; set; }
    public TestType TestType { get; set; }
}